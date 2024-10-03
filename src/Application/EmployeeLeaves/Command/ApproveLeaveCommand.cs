using Application.Common.MediatR;
using Application.EmployeeLeaves.Events;

namespace Application.EmployeeLeaves.Command
{
    public class ApproveLeaveCommand : IUpdateCommand
    {
        [JsonIgnore]
        public int EmployeeLeaveId { get; set; }

        public class Validator : AbstractValidator<ApproveLeaveCommand>
        {
            public Validator()
            {
                RuleFor(x => x.EmployeeLeaveId)
                    .GreaterThan(0);
            }
        }

        internal class Handler(IDbContext dbContext) : IUpdateCommandHandler<ApproveLeaveCommand>
        {
            public async Task<bool> Handle(ApproveLeaveCommand request, CancellationToken cancellationToken)
            {
                var leave = await dbContext.EmployeeLeaves
                    .FirstOrDefaultAsync(x => x.Id == request.EmployeeLeaveId, cancellationToken);

                leave?.Approve();

                leave?.AddDomainEvent(new LeaveApprovedEvent
                {
                    EmployeeLeaveId = leave.Id,
                    CompanyId = leave.CompanyId,
                    RetryOnError = true
                });
                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
