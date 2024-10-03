using Application.Common.MediatR;
using Application.Companies.Events;
using Application.Users.Commands;

namespace Application.Companies.Commands
{
    public class AddCompanyCommand : AddUpdateCommand
    {
        public string Email { get; set; } = null!;

        public new class Validator : AbstractValidator<AddCompanyCommand>
        {
            public Validator(IDbContext dbContext)
            {
                RuleFor(x => x)
                    .SetValidator(new AddUpdateCommand.Validator());

                RuleFor(x => x.Email)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .CustomAsync(async (email, context, cancellationToken) =>
                    {
                        var exists = await dbContext.Users
                            .AnyAsync(x => x.Email == email.ToLower(), cancellationToken);

                        if (exists)
                        {
                            context.AddFailure($"Email '${email}' is already in use.");
                        }
                    });
            }
        }

        internal class Handler(IDbContext dbContext, IMediator mediator) : IMediatRCommandHandler<AddCompanyCommand, int>
        {
            public async Task<MediatRResponse<int>> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var company = Company.Create(
                        request.Name,
                        request.Email.ToValueObject<Email>(),
                        request.PhoneNumber.ToValueObject<PhoneNumber>(),
                        request.AdministratorName,
                        request.Address);

                    await dbContext.Companies.AddAsync(company, cancellationToken);

                    await dbContext.SaveChangesAsync(cancellationToken);

                    var userId = await mediator.Send(new AddUserCommand
                    {
                        Email = request.Email,
                        Name = company.AdministratorName,
                        PhoneNumber = company.PhoneNumber.Value,
                        Role = RoleName.CompanyAdmin.ToString(),
                        CompanyId = company.Id,
                        UseTransaction = false,
                    },
                    cancellationToken);

                    company.AddDomainEvent(new CompanyCreatedEvent
                    {
                        UserId = userId
                    });

                    await transaction.CommitAsync(cancellationToken);

                    return MediatRResponse<int>.Success(company.Id);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
