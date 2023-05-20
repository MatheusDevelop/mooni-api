using System.ComponentModel.DataAnnotations.Schema;

namespace Mooni.Domain.VOs
{
    public class Amount
    {
        public string Currency { get; private set; }
        public double Value { get; set; }
        [NotMapped]
        public string FormatedValue { get; set; }
    }
}
