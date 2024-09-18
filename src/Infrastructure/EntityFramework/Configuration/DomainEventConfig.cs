namespace Infrastructure.EntityFramework.Configuration
{
    internal class DomainEventConfig : IEntityTypeConfiguration<DomainEvent>
    {
        public void Configure(EntityTypeBuilder<DomainEvent> builder)
        {
            builder.ToTable("domain_events");

            builder
                .Property(x => x.Id)
                .HasColumnName("id");

            builder
                .Property(x => x.Type)
                .HasColumnName("type");

            builder
                .Property(x => x.JSON)
                .HasColumnName("json")
                .HasColumnType("jsonb");

            builder
                .Property(x => x.OccurredOn)
                .HasColumnName("occurred_on");

            builder
                .Property(x => x.ProcessStartedOnUtc)
                .HasColumnName("process_started_on");

            builder
                .Property(x => x.ProcessCompletedOnUtc)
                .HasColumnName("process_completed_on");

            builder
                .Property(x => x.RetryCount)
                .HasColumnName("retry_count");

            builder
                .Property(x => x.RetryOnError)
                .HasColumnName("retry_on_error");

            builder
                .Property(x => x.Error)
                .HasColumnName("error");
        }
    }
}
