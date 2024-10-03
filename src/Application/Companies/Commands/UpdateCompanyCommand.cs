using Application.Common.MediatR;

namespace Application.Companies.Commands
{
    public class UpdateCompanyCommand : AddUpdateCommand
    {
        [JsonIgnore]
        public int CompanyId { get; set; }

        public new class Validator : AbstractValidator<UpdateCompanyCommand>
        {
            public Validator()
            {
                RuleFor(x => x.CompanyId)
                    .NotEmpty();

                RuleFor(x => x)
                    .SetValidator(new AddUpdateCommand.Validator())
                    .When(x => x.CompanyId > 0);
            }
        }

        internal class Handler(IDbContext dbContext) : IMediatRCommandHandler<UpdateCompanyCommand, int>
        {
            public async Task<MediatRResponse<int>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
            {
                var company = await dbContext.Companies
                        .FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);

                if (company != null)
                {
                    company.Update(request.Name, request.AdministratorName, request.PhoneNumber.ToValueObject<PhoneNumber>(), request.Address);
                    if (await dbContext.SaveChangesAsync(cancellationToken) > 0)
                    {
                        return MediatRResponse<int>.Success(company.Id);
                    }

                    return MediatRResponse<int>.Failed("No records were updated.");
                }
                else
                {
                    return MediatRResponse<int>.Failed("No company found with the supplied Id.");
                }
            }
        }
    }
}
