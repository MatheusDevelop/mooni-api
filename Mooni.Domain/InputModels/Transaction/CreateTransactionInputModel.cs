using Mooni.Domain.Entities;
using Mooni.Domain.VOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooni.Domain.InputModels.Transaction
{
    public class CreateTransactionInputModel
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public int? IconId { get; set; }
        public string? IconColor { get; set; }
        public TransactionType Type { get; set; }
        public Guid CategoryId { get; set; }
        public string Amount { get; set; }
        public string AmountCurrency { get; set; }
        public DateTime Date { get; set; }
        public bool AsOverdueDate { get; set; }
        public bool Paid { get; set; }
        public string Description { get; set; }
    }
}
