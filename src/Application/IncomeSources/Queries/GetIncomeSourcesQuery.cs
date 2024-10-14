namespace Application.IncomeSources.Queries;

public class GetIncomeSourcesQuery : PagedQuery<GetIncomeSourcesQuery.Response>
{
    public class Response
    {
        public required int IncomeSourceId { get; set; }

        public required string Name { get; set; } = null!;

        public required string Description { get; set; } = null!;
    }

    internal class Handler(IDbContext dbContext) : IRequestHandler<GetIncomeSourcesQuery, PagedResponse<Response>>
    {
        public async Task<PagedResponse<Response>> Handle(GetIncomeSourcesQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.IncomeSources
                .Select(x => new Response
                {
                    IncomeSourceId = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .OrderByDescending(x => x.IncomeSourceId)
                .ToPagedResponseAsync(request, cancellationToken);
        }
    }
}
