namespace Application.IncomeSources.Commands;

public class UpdateIncomeSourceCommand : IMediatRCommand
{
    [JsonIgnore]
    public int IncomeSourceId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public class Validator : AbstractValidator<UpdateIncomeSourceCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IncomeSourceId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }

    internal class Handler(IDbContext dbContext, ICurrentUser currentUser) : IMediatRCommandHandler<UpdateIncomeSourceCommand>
    {
        public async Task<MediatRResponse> Handle(UpdateIncomeSourceCommand request, CancellationToken cancellationToken)
        {
            var company = await dbContext.Companies
                .Include(x => x.IncomeSources)
                .FirstOrDefaultAsync(c => c.Id == currentUser.CompanyId, cancellationToken);

            company?.UpdateIncomeSource(request.IncomeSourceId, request.Name, request.Description);

            return await dbContext.SaveChangesAsync(cancellationToken) > 0 ?
                MediatRResponse.Success() :
                MediatRResponse<int>.Failed("Couldn't update income source.");
        }
    }
}
