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
        public double CurrentBalance { get; set; }
        public ComparisionPercentage Comparision { get; set; }
        public List<ChartDataViewModel<string, double>> ChartData { get; set; }
    }
}
