using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooni.Domain.ViewModels.Shared
{
    public class ChartDataViewModel<X, Y>
    {
        public X Xaxis { get; set; }
        public Y YAxis { get; set; }
    }
}
