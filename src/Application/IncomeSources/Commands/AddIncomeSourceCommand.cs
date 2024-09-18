using Domain.Finance;

namespace Application.IncomeSources.Commands;

public class AddIncomeSourceCommand : IAddCommand<int>
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

    internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IAddCommandHandler<AddIncomeSourceCommand, int>
    {
        public async Task<int> Handle(AddIncomeSourceCommand request, CancellationToken cancellationToken)
        {
            var company = await dbContext.Companies.FirstOrDefaultAsync(c => c.Id == currentUser.CompanyId, cancellationToken);

            company?.AddIncomeSource(request.Name, request.Description);

            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
