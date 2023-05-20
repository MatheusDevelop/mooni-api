using Mooni.Domain.VOs;

namespace Mooni.Domain.Entities
{
    public class Transaction:Entity
    {
        public string Name { get; set; }
        public User User { get; private set; }
        public Guid UserId { get; set; }
        public TransactionType Type { get; private set; }
        public int? IconId { get; private set; }
        public string? IconColor { get; set; }
        public Category Category { get; private set; }
        public Guid CategoryId { get; set; }
        public Amount Amount { get; set; } = new();
        public DateTime Date { get; private set; }
        public DateTime OverdueDate { get; private set; } 
        public bool Paid { get; set; }
        public string Description { get; private set; }
    }
    public enum TransactionType
    {
        Expense=0,
        Income = 1,
        GoalInvestment = 2
    }
}