using Mooni.Domain.ViewModels.Shared;

namespace Mooni.Domain.ViewModels.Analytics
{
    public class UserCostsAnalyticsViewModel
    {
        public List<ChartDataViewModel<string, double>> ChartData { get; set; }

        public UserCostsAnalyticsViewModel(List<ChartDataViewModel<string, double>> chartData)
        {
            ChartData = chartData;
        }
    }
}
