namespace Application.Regularization.Queries;

public class GetRegularizationsQuery : PagedQuery<GetRegularizationsQuery.Response>, IAuthorizeRequest
{
    public int? EmployeeId { get; set; }

    public Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken)
    {
        return Task.FromResult(currentUser.IsCompanyAdmin || !EmployeeId.HasValue || currentUser.EmployeeId == EmployeeId);
    }

    public class Validator : AbstractValidator<GetRegularizationsQuery>
    {
        public Validator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0);
        }
    }

    public class Response
    {
        public required int AttendanceRegularizationId { get; set; }

        public required int EmployeeId { get; set; }

        public required DateOnly Date { get; set; }

        public required TimeOnly ClockInTime { get; set; }

        public required TimeOnly ClockOutTime { get; set; }

        public required string Reason { get; set; } = null!;

        public required bool Approved { get; set; }
    }

    internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IRequestHandler<GetRegularizationsQuery, PagedResponse<Response>>
    {
        public async Task<PagedResponse<Response>> Handle(GetRegularizationsQuery request, CancellationToken cancellationToken)
        {
            var employeeId = request.EmployeeId ?? currentUser.EmployeeId;

            var attendanceData = await dbContext.AttendanceRegularizations
                .Where(x => x.EmployeeId == employeeId)
                .Select(x => new Response
                {
                    AttendanceRegularizationId = x.Id,
                    EmployeeId = x.EmployeeId,
                    Date = x.Date,
                    ClockInTime = x.ClockInTime,
                    ClockOutTime = x.ClockOutTime,
                    Reason = x.Reason,
                    Approved = x.Approved
                })
                .OrderByDescending(x => x.Date)
                .ToPagedResponseAsync(request, cancellationToken);

            return attendanceData;
        }
    }
}
