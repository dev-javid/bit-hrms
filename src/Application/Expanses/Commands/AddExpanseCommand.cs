using Application.Common.Validators;
using Domain.Finance;

namespace Application.Expanses.Commands;

public class AddExpanseCommand : IAddCommand<int>
{
    public decimal Amount { get; set; }

    public string[] Documents { get; set; } = [];

    public string Purpose { get; set; } = null!;

    public class Validator : AbstractValidator<AddExpanseCommand>
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

    internal class Handler(IDbContext dbContext, IFileService fileService, ICurrentUser currentUser) : IRequestHandler<AddExpanseCommand, int>
    {
        public async Task<int> Handle(AddExpanseCommand request, CancellationToken cancellationToken)
        {
            var company = await dbContext.Companies
                .FirstOrDefaultAsync(x => x.Id == currentUser.CompanyId, cancellationToken);
            var fileNames = new List<FileName>();

            foreach (var document in request.Documents)
            {
                fileNames.Add(await fileService.SaveBase64StringAsFileAsync(document, "expanses", cancellationToken));
            }

            company?.AddExpanse(request.Amount, request.Purpose, fileNames);
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
