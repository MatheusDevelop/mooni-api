namespace Mooni.Domain.Entities
{
    public class User:Entity
    {
        public User(string firstName, string lastName, string avatarUrl, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            AvatarUrl = avatarUrl;
            Email = email;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public string Email { get; set; }
        public List<Transaction> Transactions { get; set; } = new();
        public List<Goal> Goals { get; set; } = new();
    }
}
