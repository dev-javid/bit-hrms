using Application.Common.Configuration;
using Domain.Common.Abstractions;

namespace Application.EmployeeLeaves.Events;

public class LeaveAppliedEvent : IDomainEvent
{
    public string EmployeeName { get; init; } = null!;

    public DateOnly From { get; init; }

    public DateOnly To { get; init; }

    public bool RetryOnError => true;

    internal class Handler(WhatsAppConfiguration whatsAppConfig, IWhatsAppService whatsAppService) : INotificationHandler<LeaveAppliedEvent>
    {
        public async Task Handle(LeaveAppliedEvent notification, CancellationToken cancellationToken)
        {
            await whatsAppService.SendAsync(
                whatsAppConfig.LeaveAppliedTemplate,
                [notification.EmployeeName, notification.From.ToString("yyyy-MM-dd"), notification.To.ToString("yyyy-MM-dd"), notification.To.DayNumber - notification.From.DayNumber + 1],
                cancellationToken);
        }
    }
}
