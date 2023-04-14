using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooni.Domain.ViewModels.Shared
{
    public class ChartDataViewModel<X, Y>
    {
        public ChartDataViewModel(X xaxis, Y yAxis)
        {
            XAxis = xaxis;
            YAxis = yAxis;
        }

        public X XAxis { get; set; }
        public Y YAxis { get; set; }
    }
}
