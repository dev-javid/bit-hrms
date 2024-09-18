using Application.Attendance.Queries;
using Application.EmployeeLeaves.Queries;
using Application.Holidays.Queries;
using static Domain.Employees.EmployeeLeave;

namespace Application.Salaries.Queries
{
    public class GetEstimatedSaleryQuery : IRequest<GetEstimatedSaleryQuery.Response?>, IAuthorizeRequest
    {
        public int? EmployeeId { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(currentUser.IsCompanyAdmin || !EmployeeId.HasValue || currentUser.EmployeeId == EmployeeId);
        }

        public class Response
        {
            public required int EmployeeId { get; set; }

            public required decimal Compensation { get; set; }

            public required IEnumerable<Deduction> Deductions { get; set; }

            public required decimal AmountDeducted { get; set; }

            public required decimal NetAmount { get; set; }

            public required int Month { get; set; }

            public required int Year { get; set; }

            public class Deduction
            {
                public required string DeductionType { get; set; }

                public required DateOnly DeductionDate { get; set; }

                public required decimal Amount { get; set; }
            }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser, IMediator mediator) : IRequestHandler<GetEstimatedSaleryQuery, Response?>
        {
            public async Task<Response?> Handle(GetEstimatedSaleryQuery request, CancellationToken cancellationToken)
            {
                request.Month ??= DateTime.UtcNow.Month;
                request.Year ??= DateTime.UtcNow.Year;
                var employeeId = request.EmployeeId ?? currentUser.EmployeeId;

                var (compensation, attendance, leaves, holidays) = await FetchDataAsync(dbContext, mediator, request, employeeId, cancellationToken);

                var dayOfMonth = new DateOnly(request.Year.Value, request.Month.Value, 1);
                var lastDayofMonth = dayOfMonth.LastDayOfMonth();
                var deductions = new List<Response.Deduction>();

                while (dayOfMonth <= lastDayofMonth)
                {
                    if (!dayOfMonth.IsWeeklyOff() && !holidays.Contains(dayOfMonth) && !leaves.Contains(dayOfMonth))
                    {
                        var inOutTiming = attendance.FirstOrDefault(a => a.Date == dayOfMonth);
                        if (inOutTiming?.ClockOutTime == default)
                        {
                            deductions.Add(new Response.Deduction
                            {
                                DeductionType = DeductionType.Absent.ToString(),
                                DeductionDate = dayOfMonth,
                                Amount = compensation / DateTime.DaysInMonth(DateTime.Now.Year, request.Month.Value)
                            });
                        }
                    }

                    dayOfMonth = dayOfMonth.AddDays(1);
                }

                return new Response
                {
                    EmployeeId = employeeId,
                    Compensation = compensation,
                    Deductions = deductions,
                    AmountDeducted = deductions.Sum(d => d.Amount),
                    NetAmount = compensation - deductions.Sum(d => d.Amount),
                    Month = request.Month.Value,
                    Year = request.Year.Value
                };
            }

            private static async Task<(decimal Compensation, IEnumerable<GetAttendanceQuery.Response> Attendance, List<DateOnly> Leaves, List<DateOnly> HolidayDates)>
                FetchDataAsync(IDbContext dbContext, IMediator mediator, GetEstimatedSaleryQuery request, int employeeId, CancellationToken cancellationToken)
            {
                var compensation = await dbContext.Compensations
                    .Where(c => c.EmployeeId == employeeId && c.EffectiveFrom.Month <= request.Month && c.EffectiveFrom.Year <= request.Year)
                    .OrderByDescending(c => c.EffectiveFrom)
                    .Select(c => c.Amount)
                    .FirstOrDefaultAsync(cancellationToken);

                var attendanceRecords = await mediator.Send(
                    new GetAttendanceQuery
                    {
                        EmployeeId = employeeId,
                        Month = request.Month,
                        Year = request.Year,
                    },
                    cancellationToken);

                var approvedLeaves = (await mediator.Send(new GetEmployeeLeavesQuery { EmployeeId = employeeId }, cancellationToken))
                    .Items
                    .Where(l => l.Status == LeaveStatus.Approved.ToString())
                    .SelectMany(l => Enumerable.Range(0, (l.To.DayNumber - l.From.DayNumber) + 1)
                    .Select(offset => l.From.AddDays(offset)))
                    .ToList();

                var holidayDates = (await mediator.Send(new GetHolidaysQuery(), cancellationToken))
                    .Items
                    .Select(h => h.Date)
                    .ToList();

                return (compensation, attendanceRecords, approvedLeaves, holidayDates);
            }
        }
    }
}
