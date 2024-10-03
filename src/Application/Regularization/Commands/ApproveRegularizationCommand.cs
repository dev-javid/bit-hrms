using Application.Common.MediatR;

namespace Application.Regularization.Commands
{
    public class ApproveRegularizationCommand : IUpdateCommand
    {
        [JsonIgnore]
        public int AttendanceRegularizationId { get; set; }

        public class Validator : AbstractValidator<ApproveRegularizationCommand>
        {
            public Validator()
            {
                RuleFor(x => x.AttendanceRegularizationId)
                    .GreaterThan(0);
            }
        }

        internal class Handler(IDbContext dbContext) : IUpdateCommandHandler<ApproveRegularizationCommand>
        {
            public async Task<bool> Handle(ApproveRegularizationCommand request, CancellationToken cancellationToken)
            {
                var regularization = await dbContext.AttendanceRegularizations
                    .Where(ar => ar.Id == request.AttendanceRegularizationId)
                    .Select(x => new
                    {
                        x.EmployeeId,
                        x.Date,
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (regularization == null)
                {
                    return false;
                }

                var employee = await dbContext.Employees
                    .Include(e => e.AttendanceRegularizations)
                    .Include(e => e.ClockInOutTimings.Where(c => c.Date == regularization.Date))
                    .FirstOrDefaultAsync(e => e.Id == regularization.EmployeeId, cancellationToken);

                employee?.ApproveRegularization(request.AttendanceRegularizationId);

                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
