using Application.Common.MediatR;

namespace Application.IncomeSources.Commands;

public class AddIncomeSourceCommand : IMediatRCommand<int>
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public class Validator : AbstractValidator<AddIncomeSourceCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }

    internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IMediatRCommandHandler<AddIncomeSourceCommand, int>
    {
        public async Task<MediatRResponse<int>> Handle(AddIncomeSourceCommand request, CancellationToken cancellationToken)
        {
            var company = await dbContext.Companies
                .Include(x => x.IncomeSources)
                .SingleAsync(c => c.Id == currentUser.CompanyId, cancellationToken);

            var incomeSource = company.AddIncomeSource(request.Name, request.Description);

            await dbContext.SaveChangesAsync(cancellationToken);

            return incomeSource.Id > 0 ?
                MediatRResponse<int>.Success(incomeSource.Id) :
                MediatRResponse<int>.Failed("Couldn't add income source");
        }
    }
}
