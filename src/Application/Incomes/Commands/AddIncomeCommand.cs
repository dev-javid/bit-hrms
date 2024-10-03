using Application.Common.MediatR;
using Application.Common.Validators;

namespace Application.Incomes.Commands;

public class AddIncomeCommand : IAddCommand<int>
{
    public int IncomeSourceId { get; set; }

    public decimal Amount { get; set; }

    public string[] Documents { get; set; } = [];

    public string? Remarks { get; set; }

    public class Validator : AbstractValidator<AddIncomeCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IncomeSourceId)
                .GreaterThan(0);

            RuleFor(x => x.Amount)
                .GreaterThan(0);

            RuleFor(x => x.Documents)
                .NotEmpty()
                .ForEach(x => x.SetValidator(new Base64FileValidator()));
        }
    }

    internal class Handler(IDbContext dbContext, IFileService fileService) : IRequestHandler<AddIncomeCommand, int>
    {
        public async Task<int> Handle(AddIncomeCommand request, CancellationToken cancellationToken)
        {
            var incomeSource = await dbContext.IncomeSources
                .FirstOrDefaultAsync(x => x.Id == request.IncomeSourceId, cancellationToken);

            var fileNames = new List<FileName>();

            foreach (var document in request.Documents)
            {
                fileNames.Add(await fileService.SaveBase64StringAsFileAsync(document, "incomes", cancellationToken));
            }

            incomeSource?.AddIncome(request.Amount, [.. fileNames], request.Remarks);
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
