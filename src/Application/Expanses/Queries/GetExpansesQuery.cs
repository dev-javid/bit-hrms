namespace Application.Expanses.Queries;

public class GetExpansesQuery : PagedQuery<GetExpansesQuery.Response>
{
    public class Response
    {
        public required int ExpanseId { get; set; }

        public required int CompanyId { get; set; }

        public required decimal Amount { get; set; }

        public required string[] Documents { get; set; }

        public required string? Purpose { get; set; }

        public required DateTime CreatedOn { get; set; }

        internal FileName[] ExpanseDocuments { get; set; } = [];
    }

    internal class Handler(IDbContext dbContext, IFileService fileService) : IRequestHandler<GetExpansesQuery, PagedResponse<Response>>
    {
        public async Task<PagedResponse<Response>> Handle(GetExpansesQuery request, CancellationToken cancellationToken)
        {
            var expanses = await dbContext.Expanses
                .Select(ex => new Response
                {
                    ExpanseId = ex.Id,
                    CompanyId = ex.CompanyId,
                    Amount = ex.Amount,
                    ExpanseDocuments = ex.Documents.ToArray(),
                    Purpose = ex.Purpose,
                    CreatedOn = ex.CreatedOn,
                    Documents = Array.Empty<string>()
                })
                .OrderByDescending(ex => ex.CreatedOn)
                .ToPagedResponseAsync(request, cancellationToken);

            foreach (var expanse in expanses.Items)
            {
                expanse.Documents = expanse.ExpanseDocuments.Select(d => fileService.GetFileUrl(d, $"Expanses/{expanse.ExpanseId}")).ToArray();
            }

            return expanses;
        }
    }
}
