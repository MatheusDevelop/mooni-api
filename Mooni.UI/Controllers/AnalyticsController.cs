using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mooni.Domain.ViewModels.Analytics;
using Mooni.Domain.ViewModels.Shared;
using Mooni.Infrastructure.Context;
using System.Globalization;

namespace Mooni.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly MooniContext _context;
        public AnalyticsController(MooniContext context)
        {
            _context = context;
        }
        [HttpGet("currentBalance")]
        public async Task<ActionResult<UserCurrentBalanceAnalyticsViewModel>> GetCurrentBalance(int month, int year)
        {
            var transactions = await _context.Transactions.AsNoTracking().Select(e => new { e.Type, e.Amount, e.OverdueDate }).ToListAsync();
            var incomes = transactions.Where(e => e.Type == Domain.Entities.TransactionType.Income);
            var expenses = transactions.Where(e => e.Type == Domain.Entities.TransactionType.Expense);

            double incomesSum = incomes.Sum(e => e.Amount.Value);
            double expensesSum = expenses.Sum(e => e.Amount.Value);
            double currentBalance = incomesSum - expensesSum;
            var chartData = new List<ChartDataViewModel<string, double>>();

            var monthTransactions = transactions.Where(e => e.OverdueDate.Month == month && e.OverdueDate.Year == year).ToList();
            double monthBalance = 0;
            for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
            {
                transactions.Where(e => e.OverdueDate.Day == day).ToList().ForEach(t =>
                {
                    if (t.Type == Domain.Entities.TransactionType.Expense)
                        monthBalance -= t.Amount.Value;
                    if (t.Type == Domain.Entities.TransactionType.Income)
                        monthBalance += t.Amount.Value;
                });
                string xAxis = day.ToString();
                chartData.Add(new(xAxis, monthBalance));
            }
            var viewModel = new UserCurrentBalanceAnalyticsViewModel(currentBalance.ToString("C", CultureInfo.CurrentCulture), chartData);
            return Ok(viewModel);
        }
        [HttpGet("expenses")]
        public async Task<ActionResult<UserCurrentBalanceAnalyticsViewModel>> GetExpenses(int month, int year)
        {
            var expenses = await _context.Transactions.AsNoTracking().Select(e => new { e.Type, e.Amount, e.OverdueDate }).Where(e=> e.Type == Domain.Entities.TransactionType.Expense).ToListAsync();
            double expensesSum = expenses.Sum(e => e.Amount.Value);
            var chartData = new List<ChartDataViewModel<string, double>>();

            var monthExpenses = expenses.Where(e => e.OverdueDate.Month == month && e.OverdueDate.Year == year).ToList();
            for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
            {
                double dayExpense = 0;
                expenses.Where(e => e.OverdueDate.Day == day).ToList().ForEach(t =>
                {
                    if (t.Type == Domain.Entities.TransactionType.Expense)
                        dayExpense += t.Amount.Value;
                });
                string xAxis = day.ToString();
                chartData.Add(new(xAxis, dayExpense));
            }
            var viewModel = new UserCurrentBalanceAnalyticsViewModel(expensesSum.ToString("C", CultureInfo.CurrentCulture), chartData);
            return Ok(viewModel);
        }
    }
}
