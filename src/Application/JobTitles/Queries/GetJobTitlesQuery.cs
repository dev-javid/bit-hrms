namespace Application.JobTitles.Queries
{
    public class GetJobTitlesQuery : PagedQuery<GetJobTitlesQuery.Response>
    {
        public int? DepartmentId { get; set; }

        public class Response
        {
            public required int JobTitleId { get; set; }

            public required string Name { get; set; }

            public required int DepartmentId { get; set; }

            public required string DepartmentName { get; set; }
        }

        internal class Handler(IDbContext dbContext) : IRequestHandler<GetJobTitlesQuery, PagedResponse<Response>>
        {
            public async Task<PagedResponse<Response>> Handle(GetJobTitlesQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.JobTitles
                   .Where(x => request.DepartmentId == null || x.DepartmentId == request.DepartmentId)
                   .Select(x => new Response
                   {
                       JobTitleId = x.Id,
                       Name = x.Name,
                       DepartmentId = x.DepartmentId,
                       DepartmentName = x.Department.Name
                   })
                   .OrderBy(x => x.JobTitleId)
                   .ToPagedResponseAsync(request, cancellationToken);
            }
        }
    }
}
