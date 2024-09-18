using Domain.Common.Abstractions;
using Domain.Employees;

namespace Application.EmployeeLeaves.Events
{
    internal class LeaveApprovedEvent : IDomainEvent
    {
        public int EmployeeLeaveId { get; init; }

        public int CompanyId { get; init; }

        public bool RetryOnError { get; set; }

        internal class Handler(IEmailService emailService, IDbContext dbContext, IStaticContentReader staticContentReader) : INotificationHandler<LeaveApprovedEvent>
        {
            public async Task Handle(LeaveApprovedEvent notification, CancellationToken cancellationToken)
            {
                var leave = await dbContext.EmployeeLeaves
                    .IgnoreQueryFilters()
                    .Where(e => e.Id == notification.EmployeeLeaveId && e.CompanyId == notification.CompanyId)
                    .Select(e => new
                    {
                        e.From,
                        e.To,
                        e.Status,
                        e.Employee.CompanyEmail,
                        e.Employee.FullName,
                        e.Remarks
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (leave == null)
                {
                    return;
                }

                var emailText = await staticContentReader.ReadContentAsync("email-templates/leave-approved-declined.html");
                emailText = emailText.Replace("[From Date]", leave.From.ToString("yyyy-MM-dd"));
                emailText = emailText.Replace("[To Date]", leave.To.ToString("yyyy-MM-dd"));
                emailText = emailText.Replace("[Leave Status]", leave.Status.ToString());
                emailText = emailText.Replace("[User Name]", leave.FullName.ToString());
                emailText = emailText.Replace("[Remarks]", leave.Remarks);
                emailService.Send(leave.CompanyEmail, "Leave approved", emailText);
            }
        }
    }
}
