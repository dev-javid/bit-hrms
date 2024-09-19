namespace Application.Expenses.Queries;

public class GetExpensesQuery : PagedQuery<GetExpensesQuery.Response>
{
    public class Response
    {
        public required int ExpenseId { get; set; }

        public required int CompanyId { get; set; }

        public required decimal Amount { get; set; }

        public required string[] Documents { get; set; }

        public required string? Purpose { get; set; }

        public required DateTime CreatedOn { get; set; }

        internal FileName[] ExpenseDocuments { get; set; } = [];
    }

    internal class Handler(IDbContext dbContext, IFileService fileService) : IRequestHandler<GetExpensesQuery, PagedResponse<Response>>
    {
        public async Task<PagedResponse<Response>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
        {
            var expenses = await dbContext.Expenses
                .Select(ex => new Response
                {
                    ExpenseId = ex.Id,
                    CompanyId = ex.CompanyId,
                    Amount = ex.Amount,
                    ExpenseDocuments = ex.Documents.ToArray(),
                    Purpose = ex.Purpose,
                    CreatedOn = ex.CreatedOn,
                    Documents = Array.Empty<string>()
                })
                .OrderByDescending(ex => ex.CreatedOn)
                .ToPagedResponseAsync(request, cancellationToken);

            foreach (var expense in expenses.Items)
            {
                expense.Documents = expense.ExpenseDocuments.Select(d => fileService.GetFileUrl(d, $"Expenses/{expense.ExpenseId}")).ToArray();
            }

            return expenses;
        }
    }
}
