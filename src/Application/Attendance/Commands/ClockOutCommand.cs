using Application.Common.MediatR;

namespace Application.Attendance.Commands
{
    public class ClockOutCommand : IUpdateCommand
    {
        internal class Handler(IDbContext dbContext, ICurrentUser currentUser, IConfiguration configuration) : IUpdateCommandHandler<ClockOutCommand>
        {
            public async Task<bool> Handle(ClockOutCommand request, CancellationToken cancellationToken)
            {
                var today = DateOnly.FromDateTime(DateTime.Now);

                var employee = await dbContext.Employees
                    .Include(e => e.ClockInOutTimings.Where(t => t.Date == today))
                    .FirstOrDefaultAsync(e => e.Id == currentUser.EmployeeId, cancellationToken);

                if (employee == null)
                {
                    return false;
                }

                var workHours = configuration.GetValue<int>("Attendance:WorkHours");
                employee.Clockout(workHours);
                await dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
        }
    }
}
