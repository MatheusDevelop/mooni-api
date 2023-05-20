using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mooni.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooni.Infrastructure.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.User)
                    .WithMany(e => e.Transactions)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(e => e.Category)
                   .WithMany()
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(x => x.Amount)
                .Property(x => x.Value)
                .HasColumnName("AmountValue");
            builder.OwnsOne(x => x.Amount)
               .Property(x => x.Currency)
               .HasColumnName("AmountCurrency");
        }
    }
}
