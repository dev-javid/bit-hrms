namespace Application.Reports.Queries
{
    public class GetAdminBasicReportQuery : IRequest<GetAdminBasicReportQuery.Response>
    {
        public class Response
        {
            public required int Departments { get; set; }

            public required int Companies { get; set; }

            public required int Employees { get; set; }

            public required int Holidays { get; set; }
        }

        internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IRequestHandler<GetAdminBasicReportQuery, Response>
        {
            public async Task<Response> Handle(GetAdminBasicReportQuery request, CancellationToken cancellationToken)
            {
                return new Response
                {
                    Companies = currentUser.IsSuperAdmin ? await dbContext.Companies.CountAsync(cancellationToken) : 0,
                    Departments = await dbContext.Departments.CountAsync(cancellationToken),
                    Employees = await dbContext.Employees.CountAsync(cancellationToken),
                    Holidays = await dbContext.Holidays.CountAsync(cancellationToken)
                };
            }
        }
    }
}
