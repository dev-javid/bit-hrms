using Application.Common.MediatR;

namespace Application.Companies.Commands
{
    public class DeleteCompanyCommand : IMediatRCommand
    {
        public int CompanyId { get; set; }

        public class Validator : AbstractValidator<DeleteCompanyCommand>
        {
            public Validator()
            {
                RuleFor(x => x.CompanyId)
                    .GreaterThan(0);
            }
        }

        internal class Handler(IDbContext dbContext) : IMediatRCommandHandler<DeleteCompanyCommand>
        {
            public async Task<MediatRResponse> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext.Companies
                    .Where(x => !x.IsDeleted)
                    .FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);

                if (company != null)
                {
                    company.SoftDelete();
                }

                if (await dbContext.SaveChangesAsync(cancellationToken) > 0)
                {
                    return MediatRResponse.Success();
                }

                return MediatRResponse.Failed("Company not deleted.");
            }
        }
    }
}
