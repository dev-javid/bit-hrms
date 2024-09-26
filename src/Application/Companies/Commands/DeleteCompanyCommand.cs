namespace Application.Companies.Commands
{
    public class DeleteCompanyCommand : IDeleteCommand
    {
        public int CompanyId { get; set; }

        public class Validator : AbstractValidator<DeleteCompanyCommand>
        {
            public Validator()
            {
                RuleFor(x => x.CompanyId)
                    .NotEmpty();
            }
        }

        internal class Handler(IDbContext dbContext) : IDeleteCommandHandler<DeleteCompanyCommand>
        {
            public async Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext.Companies
                    .FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);

                if (company != null)
                {
                    company.SoftDelete();
                }

                return await dbContext.SaveChangesAsync(cancellationToken) > 0;
            }
        }
    }
}
