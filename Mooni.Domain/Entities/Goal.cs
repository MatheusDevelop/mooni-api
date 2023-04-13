namespace Mooni.Domain.Entities
{
    public class Goal:Entity
    {
        public string Name { get; private set; }
        public double TotalAmount { get; private set; }
        public double CurrentAmount { get; private set; }
        public Category Category { get; private set; }
        public DateTime EstimatedDate { get; private set; }
        public string Description { get; private set; }
    }
}