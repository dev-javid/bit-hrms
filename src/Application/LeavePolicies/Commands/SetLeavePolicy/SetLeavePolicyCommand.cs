using Application.Common.MediatR;

namespace Application.LeavePolicies.Commands.SetLeavePolicy
{
    public class SetLeavePolicyCommand : IUpdateCommand
    {
        public int CasualLeaves { get; set; }

        public double EarnedLeavesPerMonth { get; set; }

        public int Holidays { get; set; }

        public class Validator : AbstractValidator<SetLeavePolicyCommand>
        {
            public Validator()
            {
                RuleFor(x => x.CasualLeaves)
                    .GreaterThanOrEqualTo(0)
                    .LessThanOrEqualTo(100);

                RuleFor(x => x.EarnedLeavesPerMonth)
                    .GreaterThanOrEqualTo(0)
                    .LessThanOrEqualTo(10);

                RuleFor(x => x.Holidays)
                    .GreaterThanOrEqualTo(0)
                    .LessThanOrEqualTo(100);
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IUpdateCommandHandler<SetLeavePolicyCommand>
        {
            public async Task<bool> Handle(SetLeavePolicyCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext
                    .Companies
                    .Include(x => x.LeavePolicy)
                    .Where(x => x.Id == currentUser.CompanyId)
                    .FirstAsync(cancellationToken);

                company.SetLeavePolicy(request.CasualLeaves, request.EarnedLeavesPerMonth, request.Holidays);

                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
