namespace Infrastructure.EntityFramework
{
    internal class DomainEvent
    {
        private DomainEvent()
        {
        }

        public Guid Id { get; private set; }

        public string Type { get; private set; } = null!;

        public string JSON { get; private set; } = null!;

        public DateTime OccurredOn { get; private set; }

        public DateTime? ProcessStartedOnUtc { get; private set; }

        public DateTime? ProcessCompletedOnUtc { get; private set; }

        public string? Error { get; private set; }

        public int RetryCount { get; private set; }

        public bool RetryOnError { get; private set; }

        public static DomainEvent Create(Type type, object @object, bool retryOnError)
        {
            return new DomainEvent
            {
                Id = Guid.NewGuid(),
                Type = type.AssemblyQualifiedName!,
                OccurredOn = DateTime.UtcNow,
                JSON = JsonSerializer.Serialize(@object),
                RetryOnError = retryOnError
            };
        }

        public void SetProcessStarted()
        {
            ProcessStartedOnUtc = DateTime.UtcNow;
            RetryCount += 1;
        }

        public void SetProcessCompleted()
        {
            ProcessCompletedOnUtc = DateTime.UtcNow;
        }

        public void SetError(string error)
        {
            Error = error;
        }
    }
}
