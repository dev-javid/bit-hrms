namespace Application.Incomes.Queries;

public class GetIncomesQuery : PagedQuery<GetIncomesQuery.Response>
{
    public class Response
    {
        public int IncomeId { get; set; }

        public decimal Amount { get; set; }

        public string IncomeSourceName { get; set; } = null!;

        public string[] Documents { get; set; } = [];

        public string? Remarks { get; set; }

        public DateTime CreatedOn { get; set; }

        internal FileName[] IncomeDocuments { get; set; } = [];
    }

    internal class Handler(IDbContext dbContext, IFileService fileService) : IRequestHandler<GetIncomesQuery, PagedResponse<Response>>
    {
        public async Task<PagedResponse<Response>> Handle(GetIncomesQuery request, CancellationToken cancellationToken)
        {
            var incomes = await dbContext.Incomes
                .Select(i => new Response
                {
                    IncomeId = i.Id,
                    Amount = i.Amount,
                    IncomeSourceName = i.IncomeSource.Name,
                    IncomeDocuments = i.Documents.ToArray(),
                    Remarks = i.Remarks,
                    CreatedOn = i.CreatedOn
                })
                .OrderByDescending(i => i.CreatedOn)
                .ToPagedResponseAsync(request, cancellationToken);

            foreach (var income in incomes.Items)
            {
                income.Documents = income.IncomeDocuments.Select(d => fileService.GetFileUrl(d, $"Incomes/{income.IncomeId}")).ToArray();
            }

            return incomes;
        }
    }
}
