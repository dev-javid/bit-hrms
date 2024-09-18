namespace Application.IncomeSources.Queries;

public class GetIncomeSourcesQuery : PagedQuery<GetIncomeSourcesQuery.Response>
{
    public class Response
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }

    internal class Handler(IDbContext dbContext) : IRequestHandler<GetIncomeSourcesQuery, PagedResponse<Response>>
    {
        public async Task<PagedResponse<Response>> Handle(GetIncomeSourcesQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.IncomeSources
                .Select(x => new Response
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .OrderByDescending(x => x.Id)
                .ToPagedResponseAsync(request, cancellationToken);
        }
    }
}
