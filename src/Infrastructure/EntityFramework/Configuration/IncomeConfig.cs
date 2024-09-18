using Domain.Finance;
using Infrastructure.EntityFramework.Encryption;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityFramework.Configuration
{
    internal class IncomeConfig : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.ToTable("incomes");

            builder
                .Property(x => x.AmountEncrypted)
                .IsEncrypted()
                .HasColumnName("amount");

            builder
                .Property(x => x.IncomeSourceId)
                .HasColumnName("income_source_id");

            builder
                .Property(x => x.Documents)
                .HasColumnName("documents")
                .HasConversion(
                    new ValueConverter<FileName[], string[]>(
                        v => v.Select(x => x.Value).ToArray(),
                        v => v.Select(FileName.From).ToArray()));

            builder
                .Property(x => x.Remarks)
                .HasColumnName("remarks");
        }
    }
}
