using Mooni.Domain.ViewModels.Shared;
namespace Mooni.Domain.ViewModels.Analytics
{
    public class UserCashflowAnalyticsViewModel
    {
        public List<ChartDataViewModel<string, double>> ChartData { get; set; }
        public UserCashflowAnalyticsViewModel(List<ChartDataViewModel<string, double>> chartData)
        {
            ChartData = chartData;
        }
    }
}
