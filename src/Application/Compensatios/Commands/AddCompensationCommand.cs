using Application.Common.MediatR;

namespace Application.Compensatios.Commands
{
    public class AddCompensationCommand : IAddCommand<int>
    {
        public int EmployeeId { get; set; }

        public int Amount { get; set; }

        public DateOnly EffectiveFrom { get; set; }

        public class Validator : AbstractValidator<AddCompensationCommand>
        {
            public Validator()
            {
                RuleFor(x => x.EmployeeId)
                    .GreaterThan(0);

                RuleFor(x => x.Amount)
                    .GreaterThan(0);
            }
        }

        internal class Handler(IDbContext dbContext) : IAddCommandHandler<AddCompensationCommand, int>
        {
            public async Task<int> Handle(AddCompensationCommand request, CancellationToken cancellationToken)
            {
                var employee = await dbContext
                    .Employees
                    .Include(e => e.Compensations)
                    .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken);

                if (employee == null)
                {
                    return 0;
                }

                var compensation = employee.AddCompensation(request.EmployeeId, request.EffectiveFrom, request.Amount);
                await dbContext.SaveChangesAsync(cancellationToken);
                return compensation.Id;
            }
        }
    }
}
