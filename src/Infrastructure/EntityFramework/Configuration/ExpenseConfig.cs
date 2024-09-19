using Domain.Finance;
using Infrastructure.EntityFramework.Encryption;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class ExpenseConfig : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("expenses");

            builder
                .Property(x => x.AmountEncrypted)
                .IsEncrypted()
                .HasColumnName("amount");

            builder
                .Property(x => x.Purpose)
                .HasColumnName("purpose");

            builder
                .Property(x => x.Documents)
                .HasColumnName("documents")
                .HasConversion(
                    new ValueConverter<FileName[], string[]>(
                        v => v.Select(x => x.Value).ToArray(),
                        v => v.Select(x => FileName.From(x)).ToArray()));
        }
    }
}
