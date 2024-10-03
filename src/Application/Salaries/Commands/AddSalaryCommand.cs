using Application.Common.MediatR;
using Application.Salaries.Queries;
using Domain.Salaries;

namespace Application.Salaries.Commands
{
    public class AddSalaryCommand : IAddCommand<int>
    {
        public int EmployeeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public class Validator : AbstractValidator<AddSalaryCommand>
        {
            public Validator()
            {
                RuleFor(x => x.EmployeeId)
                    .GreaterThan(0);

                RuleFor(x => x.Year)
                    .GreaterThan(2000);
            }
        }

        internal class Handler(IDbContext dbContext, IMediator mediator) : IAddCommandHandler<AddSalaryCommand, int>
        {
            public async Task<int> Handle(AddSalaryCommand request, CancellationToken cancellationToken)
            {
                var estimatedSalary = await mediator.Send(new GetEstimatedSaleryQuery
                {
                    EmployeeId = request.EmployeeId,
                    Month = request.Month
                },
                cancellationToken);

                if (estimatedSalary != null)
                {
                    var month = Domain.Common.ValueObjects.Month.From(request.Month);

                    var salary = Salary.Create(request.EmployeeId, month, request.Year, estimatedSalary.NetAmount);

                    foreach (var deduction in estimatedSalary.Deductions)
                    {
                        DeductionType deductionType = (DeductionType)Enum.Parse(typeof(DeductionType), deduction.DeductionType);
                        salary.AddDeduction(deductionType, deduction.Amount);
                    }

                    await dbContext.Salaries.AddAsync(salary, cancellationToken);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    return salary.Id;
                }

                return 0;
            }
        }
    }
}
