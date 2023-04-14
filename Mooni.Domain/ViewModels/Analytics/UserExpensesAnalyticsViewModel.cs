using Mooni.Domain.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooni.Domain.ViewModels.Analytics
{
    public class UserExpensesAnalyticsViewModel
    {
        public UserExpensesAnalyticsViewModel(string expenses, List<ChartDataViewModel<string, double>> chartData)
        {
            Expenses = expenses;
            ChartData = chartData;
        }

        public string Expenses { get; set; }
        public List<ChartDataViewModel<string, double>> ChartData { get; set; }
    }
}
