using Application.Common.MediatR;
using Application.Common.Validators;

namespace Application.Expenses.Commands;

public class AddExpenseCommand : IAddCommand<int>
{
    public decimal Amount { get; set; }

    public string[] Documents { get; set; } = [];

    public string Purpose { get; set; } = null!;

    public class Validator : AbstractValidator<AddExpenseCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0);

            RuleFor(x => x.Documents)
                .NotEmpty()
                .ForEach(x => x.SetValidator(new Base64FileValidator()));

            RuleFor(x => x.Purpose)
                .NotEmpty();
        }
    }

    internal class Handler(IDbContext dbContext, IFileService fileService, ICurrentUser currentUser) : IRequestHandler<AddExpenseCommand, int>
    {
        public async Task<int> Handle(AddExpenseCommand request, CancellationToken cancellationToken)
        {
            var company = await dbContext.Companies
                .FirstOrDefaultAsync(x => x.Id == currentUser.CompanyId, cancellationToken);
            var fileNames = new List<FileName>();

            foreach (var document in request.Documents)
            {
                fileNames.Add(await fileService.SaveBase64StringAsFileAsync(document, "expenses", cancellationToken));
            }

            company?.AddExpense(request.Amount, request.Purpose, fileNames);
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
