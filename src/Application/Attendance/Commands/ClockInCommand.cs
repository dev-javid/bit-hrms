using Application.Common.MediatR;

namespace Application.Attendance.Commands;

public class ClockInCommand : IUpdateCommand
{
    internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IUpdateCommandHandler<ClockInCommand>
    {
        public async Task<bool> Handle(ClockInCommand request, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var employee = await dbContext.Employees
                .Include(x => x.EmployeeLeaves)
                .Include(e => e.ClockInOutTimings.Where(t => t.Date == today))
                .FirstOrDefaultAsync(e => e.Id == currentUser.EmployeeId, cancellationToken);

            if (employee == null)
            {
                return false;
            }

            var holidays = await dbContext.Holidays
                    .ToListAsync(cancellationToken);

            employee.ClockIn(holidays);
            await dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
