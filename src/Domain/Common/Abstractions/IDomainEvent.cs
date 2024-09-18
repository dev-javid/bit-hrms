using MediatR;

namespace Domain.Common.Abstractions
{
    public interface IDomainEvent : INotification
    {
        public bool RetryOnError { get; }
    }
}
