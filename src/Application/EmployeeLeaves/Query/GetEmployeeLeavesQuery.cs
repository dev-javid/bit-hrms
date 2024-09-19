namespace Application.EmployeeLeaves.Queries
{
    public class GetEmployeeLeavesQuery : PagedQuery<GetEmployeeLeavesQuery.Response>, IAuthorizeRequest
    {
        public int? EmployeeId { get; set; }

        public Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(currentUser.IsCompanyAdmin || !EmployeeId.HasValue || currentUser.EmployeeId == EmployeeId);
        }

        public class Response
        {
            public required int EmployeeLeaveId { get; set; }

            public required int EmployeeId { get; set; }

            public required string EmployeeName { get; set; }

            public required DateOnly To { get; set; }

            public required DateOnly From { get; set; }

            public required DateTime CreatedOn { get; set; }

            public required string Status { get; set; }

            public required string? Remarks { get; set; }
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetEmployeeLeavesQuery, PagedResponse<Response>>
        {
            public async Task<PagedResponse<Response>> Handle(GetEmployeeLeavesQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.EmployeeLeaves
                   .Where(x => request.EmployeeId == null || x.EmployeeId == request.EmployeeId)
                   .Select(x => new Response
                   {
                       EmployeeLeaveId = x.Id,
                       EmployeeId = x.EmployeeId,
                       Status = x.Status.ToString(),
                       EmployeeName = $"{x.Employee.FirstName} {x.Employee.LastName}",
                       From = x.From,
                       CreatedOn = x.CreatedOn,
                       To = x.To,
                       Remarks = x.Remarks
                   })
                   .OrderByDescending(x => x.CreatedOn)
                   .ToPagedResponseAsync(request, cancellationToken);
            }
        }
    }
}
