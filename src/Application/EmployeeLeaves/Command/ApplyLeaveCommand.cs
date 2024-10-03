using Application.Common.MediatR;
using Application.EmployeeLeaves.Events;

namespace Application.EmployeeLeaves.Command
{
    public class ApplyLeaveCommand : IAddCommand<int>
    {
        public DateOnly From { get; set; }

        public DateOnly To { get; set; }

        public class Validator : AbstractValidator<ApplyLeaveCommand>
        {
            public Validator()
            {
                RuleFor(x => x.From)
                    .NotEmpty();

                RuleFor(x => x.To)
                    .NotEmpty();

                RuleFor(command => command)
                    .Must((command) => command.To >= command.From)
                    .WithMessage("'From' date must be earlier than or equal to the 'To' date.");
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IAddCommandHandler<ApplyLeaveCommand, int>
        {
            public async Task<int> Handle(ApplyLeaveCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext.Companies
                    .Where(x => x.Id == currentUser.CompanyId)
                    .Select(x => new
                    {
                        x.LeavePolicy,
                        x.Holidays,
                        x.FinancialMonth
                    })
                    .FirstAsync(cancellationToken);

                if (company.LeavePolicy is null)
                {
                    throw CustomException.WithBadRequest("Company must set leave policy first.");
                }

                var employee = await dbContext.Employees
                        .Include(x => x.EmployeeLeaves)
                        .Where(x => x.UserId == currentUser.Id)
                        .FirstAsync(cancellationToken);

                var leave = employee.ApplyLeave(request.From, request.To, company.LeavePolicy, company.FinancialMonth, company.Holidays);

                employee.AddDomainEvent(new LeaveAppliedEvent
                {
                    EmployeeName = employee.FullName,
                    From = leave.From,
                    To = leave.To,
                });

                await dbContext.SaveChangesAsync(cancellationToken);

                return leave.Id;
            }
        }
    }
}
