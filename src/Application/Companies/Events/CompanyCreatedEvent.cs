using Application.Users.Commands;
using Domain.Common.Abstractions;

namespace Application.Companies.Events
{
    internal class CompanyCreatedEvent : IDomainEvent
    {
        public string Email { get; set; } = null!;

        public bool RetryOnError => false;

        internal class Handler(IEmailService emailService, IDbContext dbContext, IMediator mediator, IStaticContentReader staticContentReader) : INotificationHandler<CompanyCreatedEvent>
        {
            public async Task Handle(CompanyCreatedEvent notification, CancellationToken cancellationToken)
            {
                var company = await dbContext
                            .Companies
                            .FirstAsync(x => x.Email.Value == notification.Email, cancellationToken);

                var emailText = await staticContentReader.ReadContentAsync("email-templates/company-created.html");
                emailText = emailText.Replace("[User Name]", company.AdministratorName);
                emailText = emailText.Replace("[Company Name]", "Bit Xplorer");
                emailText = emailText.Replace("[Year]", DateTime.Now.Year.ToString());
                emailService.Send(company.Email, "Company registration", emailText);

                await mediator.Send(new AddUserCommand
                {
                    Email = notification.Email,
                    Name = company.AdministratorName,
                    PhoneNumber = company.PhoneNumber.Value,
                    Role = RoleName.CompanyAdmin.ToString(),
                    CompanyId = company.Id
                },
                cancellationToken);
            }
        }
    }
}
