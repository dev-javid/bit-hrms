using Application.Common.MediatR;

namespace Application.Regularization.Commands
{
    public class AddAttendanceRegularizationCommand : IAddCommand<int>
    {
        public TimeOnly ClockOutTime { get; set; }

        public TimeOnly ClockInTime { get; set; }

        public DateOnly Date { get; set; }

        public string Reason { get; set; } = string.Empty;

        public class Validator : AbstractValidator<AddAttendanceRegularizationCommand>
        {
            public Validator()
            {
                RuleFor(x => x.ClockOutTime)
                    .NotEmpty();

                RuleFor(x => x.ClockInTime)
                    .NotEmpty();

                RuleFor(x => x)
                    .Must(x => x.ClockOutTime > x.ClockInTime);

                RuleFor(x => x.Date)
                    .NotEmpty();

                RuleFor(x => x.Reason)
                    .NotEmpty();
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser, IConfiguration configuration) : IAddCommandHandler<AddAttendanceRegularizationCommand, int>
        {
            public async Task<int> Handle(AddAttendanceRegularizationCommand request, CancellationToken cancellationToken)
            {
                var employee = await dbContext.Employees
                    .Include(e => e.ClockInOutTimings.Where(t => t.Date == request.Date))
                    .Where(x => x.Id == currentUser.EmployeeId)
                   .FirstOrDefaultAsync(cancellationToken);

                if (employee == null)
                {
                    return 0;
                }

                var workHours = configuration.GetValue<int>("Attendance:WorkHours");

                var attendanceRegularization = employee.AddAttendanceRegularization(request.Date, request.ClockInTime, request.ClockOutTime, request.Reason, workHours);

                await dbContext.SaveChangesAsync(cancellationToken);
                return attendanceRegularization.Id;
            }
        }
    }
}
