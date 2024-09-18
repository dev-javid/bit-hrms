namespace Application.Departments.Queries
{
    public class GetDepartmentQuery : IRequest<GetDepartmentQuery.Response?>, IAuthorizeRequest
    {
        public int DepartmentId { get; set; }

        public Task<bool> AuthorizeAsync(ICurrentUser currentUser, IDbContext dbContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(currentUser.IsCompanyAdmin);
        }

        public class Response
        {
            public required int DepartmentId { get; set; }

            public required string Name { get; set; }

            public required IEnumerable<JobTitle> JobTitles { get; set; }

            public class JobTitle
            {
                public required string Name { get; set; }
            }
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetDepartmentQuery, Response?>
        {
            public async Task<Response?> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Departments
                    .Where(d => d.Id == request.DepartmentId)
                    .Select(d => new Response
                    {
                        DepartmentId = d.Id,
                        Name = d.Name,
                        JobTitles = d.JobTitles.Select(j => new Response.JobTitle
                        {
                            Name = j.Name,
                        })
                    })
                    .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
