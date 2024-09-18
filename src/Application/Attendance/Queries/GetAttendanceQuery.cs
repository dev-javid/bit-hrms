namespace Application.Attendance.Queries
{
    public class GetAttendanceQuery : IRequest<IEnumerable<GetAttendanceQuery.Response>>, IAuthorizeRequest
    {
        public int? EmployeeId { get; set; }

        public DateOnly? Date { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }

        public Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(currentUser.IsCompanyAdmin || !EmployeeId.HasValue || currentUser.EmployeeId == EmployeeId);
        }

        public class Response
        {
            public required int ClockInOutId { get; set; }

            public required int EmployeeId { get; set; }

            public required DateOnly Date { get; set; }

            public required TimeOnly ClockInTime { get; set; }

            public required TimeOnly? ClockOutTime { get; set; }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IRequestHandler<GetAttendanceQuery, IEnumerable<Response>>
        {
            public async Task<IEnumerable<Response>> Handle(GetAttendanceQuery request, CancellationToken cancellationToken)
            {
                if (request.Date is null && request.Month is null)
                {
                    request.Month = DateTime.Now.Month;
                }

                if (request.Date is null && request.Year is null)
                {
                    request.Year = DateTime.Now.Year;
                }

                var employeeId = request.EmployeeId ?? currentUser.EmployeeId;

                var query = dbContext.ClockInOutTimings
                    .Where(x => x.EmployeeId == employeeId);

                if (request.Date.HasValue)
                {
                    query = query.Where(x => x.Date == request.Date.Value);
                }

                if (request.Month.HasValue)
                {
                    query = query.Where(x => x.Date.Month == request.Month.Value);
                }

                if (request.Year.HasValue)
                {
                    query = query.Where(x => x.Date.Year == request.Year.Value);
                }

                return await query.Select(x => new Response
                {
                    ClockInOutId = x.Id,
                    EmployeeId = x.EmployeeId,
                    Date = x.Date,
                    ClockInTime = x.ClockInTime,
                    ClockOutTime = x.ClockOutTime,
                })
                .ToListAsync(cancellationToken);
            }
        }
    }
}
