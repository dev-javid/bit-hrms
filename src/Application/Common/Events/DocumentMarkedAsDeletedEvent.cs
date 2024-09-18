using Domain.Common.Abstractions;

namespace Application.Common.Events
{
    internal class DocumentMarkedAsDeletedEvent : IDomainEvent
    {
        public required string FileName { get; set; }

        public required string RelativePath { get; set; }

        public bool RetryOnError { get; set; }

        internal class Handler(IFileService service) : INotificationHandler<DocumentMarkedAsDeletedEvent>
        {
            public Task Handle(DocumentMarkedAsDeletedEvent notification, CancellationToken cancellationToken)
            {
                service.DeleteFile(notification.FileName.ToValueObject<FileName>(), notification.RelativePath);
                return Task.CompletedTask;
            }
        }
    }
}
