using Microsoft.EntityFrameworkCore;
using Mooni.Domain.Entities;
using Mooni.Domain.VOs;
using Mooni.Infrastructure.Mappings;

namespace Mooni.Infrastructure.Context
{
    public class MooniContext:DbContext
    {
        public MooniContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionMapping).Assembly);
        }
        public DbSet<Category> Categories{ get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
