namespace Application.Reports.Queries
{
    public class GetEmployeeBasicReportQuery : IRequest<GetEmployeeBasicReportQuery.Response>, IAuthorizeRequest
    {
        [JsonIgnore]
        public int? EmployeeId { get; set; }

        public Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(EmployeeId == null || currentUser.IsCompanyAdmin || EmployeeId == currentUser.EmployeeId);
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IRequestHandler<GetEmployeeBasicReportQuery, Response>
        {
            public async Task<Response> Handle(GetEmployeeBasicReportQuery request, CancellationToken cancellationToken)
            {
                var company = await dbContext.Companies
                    .Where(x => x.Id == currentUser.CompanyId)
                    .Select(x => new
                    {
                        x.LeavePolicy,
                        x.FinancialMonth
                    })
                    .FirstAsync(cancellationToken);

                if (company.LeavePolicy is null)
                {
                    throw CustomException.WithBadRequest("Company must set leave policy first.");
                }

                var employee = await dbContext.Employees
                        .Include(x => x.EmployeeLeaves)
                        .Where(x => x.Id == (request.EmployeeId != null ? request.EmployeeId : currentUser.EmployeeId))
                        .FirstAsync(cancellationToken);

                return new Response
                {
                    LeavesAvailable = employee.GetAvailableLeaves(company.FinancialMonth, company.LeavePolicy),
                    LeavesConsumed = employee.GetConsumedLeaves(company.FinancialMonth)
                };
            }
        }

        internal class Response
        {
            public required double LeavesAvailable { get; set; }

            public required int LeavesConsumed { get; set; }
        }
    }
}
