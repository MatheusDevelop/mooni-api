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

            var monthTransactions = transactions.Where(e => e.OverdueDate.Month == month + 1 && e.OverdueDate.Year == year).ToList();
            double monthBalance = 0;
            for (int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
            {
                var x = monthTransactions.Where(e => e.OverdueDate.Day == day).ToList();
                var y = monthTransactions[0].OverdueDate;
                x.ForEach(t =>
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
        [HttpGet("cashflow")]
        public async Task<ActionResult<UserCurrentBalanceAnalyticsViewModel>> GetCashflow(int year)
        {
            var transactions = await _context.Transactions.AsNoTracking().Select(e => new { e.Type, e.Amount, e.OverdueDate }).ToListAsync();
            var chartData = new List<ChartDataViewModel<string, double>>();
            double yearBalance = 0;
            for (int month = 1; month <= 12; month++)
            {
                var monthTransactions = transactions.Where(e => e.OverdueDate.Month == month && e.OverdueDate.Year == year).ToList();
                var incomesSum = monthTransactions.Where(e => e.Type == Domain.Entities.TransactionType.Income).Sum(e => e.Amount.Value);
                var expensesSum = monthTransactions.Where(e => e.Type == Domain.Entities.TransactionType.Expense).Sum(e => e.Amount.Value);
                double monthBalance = incomesSum - expensesSum;
                yearBalance += monthBalance;
                ChartDataViewModel<string, double> data = new(month.ToString(), yearBalance);
                data.Metadata.Add(incomesSum.ToString());
                data.Metadata.Add(expensesSum.ToString());
                chartData.Add(data);
            }
            var viewModel = new UserCashflowAnalyticsViewModel(chartData);
            return Ok(viewModel);
        }
        [HttpGet("expenses")]
        public async Task<ActionResult<UserCurrentBalanceAnalyticsViewModel>> GetExpenses(int month, int year)
        {
            var expenses = await _context.Transactions
                    .AsNoTracking()
                    .Select(e => new { e.Type, e.Amount, e.OverdueDate })
                    .Where(e => e.Type == Domain.Entities.TransactionType.Expense && e.OverdueDate.Month == month + 1 && e.OverdueDate.Year == year)
                    .ToListAsync();
            double expensesSum = expenses.Sum(e => e.Amount.Value);
            var chartData = new List<ChartDataViewModel<string, double>>();
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
        [HttpGet("overdues")]
        public async Task<ActionResult> GetOverdues(int month, int year,int day)
        {
            var currDate = new DateTime(year,month,day);
            var expenses = await _context.Transactions.AsNoTracking().Where(e => !e.Paid && e.Date >= currDate && e.Date <= currDate.AddDays(7)).ToListAsync();
            var expensesModel = expenses.Select(e=>
            {
                e.Amount.FormatedValue = e.Amount.Value.ToString("N2", CultureInfo.CurrentCulture);
                return e;
            }).ToList();
            return Ok(expenses);
        }
        [HttpGet("costs")]
        public async Task<ActionResult<UserCurrentBalanceAnalyticsViewModel>> GetCosts(int month, int year)
        {
            var transactions = await _context.Transactions.Include(e => e.Category).AsNoTracking().Select(e => new { e.Type, e.Amount, e.OverdueDate, e.Category }).ToListAsync();
            var expenses = transactions.Where(e => e.Type == Domain.Entities.TransactionType.Expense).ToList();
            double incomeSum = transactions.Where(e => e.Type == Domain.Entities.TransactionType.Income).Sum(e => e.Amount.Value);
            var chartData = new List<ChartDataViewModel<string, double>>();

            Dictionary<string, double> percentageByCategory = new Dictionary<string, double>();
            expenses.ForEach(e =>
            {
                var data = chartData.FirstOrDefault(x => x.XAxis == e.Category.Name);
                if (data is not null)
                {
                    data.YAxis = data.YAxis += (e.Amount.Value / incomeSum) * 100;
                }
                else
                {
                    ChartDataViewModel<string, double> model = new(e.Category.Name, Math.Round((e.Amount.Value / incomeSum) * 100, 1));
                    model.Metadata.Add(e.Category.Color);
                    model.Metadata.Add(e.Category.IconId.ToString());
                    model.Metadata.Add(e.Category.Name);

                    chartData.Add(model);
                }
            });
            var viewModel = new UserCostsAnalyticsViewModel(chartData);
            return Ok(viewModel);
        }
    }
}
