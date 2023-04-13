using Mooni.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooni.Domain.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CategoryIconId { get; set; }
        public string CategoryColor { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
    }
}
