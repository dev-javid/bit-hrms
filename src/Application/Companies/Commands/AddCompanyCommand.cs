using Application.Companies.Events;
using Domain.Companies;

namespace Application.Companies.Commands
{
    public class AddCompanyCommand : IAddCommand<int>
    {
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string AdministratorName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public class Validator : AbstractValidator<AddCompanyCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(100);

                RuleFor(x => x.Email)
                    .NotEmpty();

                RuleFor(x => x.PhoneNumber)
                    .NotEmpty();

                RuleFor(x => x.AdministratorName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(100);
            }
        }

        internal class Handler(IDbContext dbContext) : IAddCommandHandler<AddCompanyCommand, int>
        {
            public async Task<int> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
            {
                var company = Company.Create(
                    request.Name,
                    request.Email.ToValueObject<Email>(),
                    request.PhoneNumber.ToValueObject<PhoneNumber>(),
                    request.AdministratorName);

                await dbContext.Companies.AddAsync(company, cancellationToken);

                company.AddDomainEvent(new CompanyCreatedEvent
                {
                    Email = company.Email.Value,
                });

                await dbContext.SaveChangesAsync(cancellationToken);

                return company.Id;
            }
        }
    }
}
