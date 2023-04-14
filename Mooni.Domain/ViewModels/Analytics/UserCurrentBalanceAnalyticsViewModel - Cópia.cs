using Mooni.Domain.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooni.Domain.ViewModels.Analytics
{
    public class UserCurrentBalanceAnalyticsViewModel
    {
        public UserCurrentBalanceAnalyticsViewModel(string currentBalance, List<ChartDataViewModel<string, double>> chartData)
        {
            CurrentBalance = currentBalance;
            ChartData = chartData;
        }

        public string CurrentBalance { get; set; }
        public List<ChartDataViewModel<string, double>> ChartData { get; set; }
    }
}
