namespace Application.Departments.Queries
{
    public class GetDepartmentsQuery : PagedQuery<GetDepartmentsQuery.Response>
    {
        public class Response
        {
            public required int DepartmentId { get; set; }

            public required string Name { get; set; } = null!;
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetDepartmentsQuery, PagedResponse<Response>>
        {
            public async Task<PagedResponse<Response>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Departments
                   .Select(x => new Response
                   {
                       DepartmentId = x.Id,
                       Name = x.Name,
                   })
                   .OrderBy(x => x.DepartmentId)
                   .ToPagedResponseAsync(request, cancellationToken);
            }
        }
    }
}
