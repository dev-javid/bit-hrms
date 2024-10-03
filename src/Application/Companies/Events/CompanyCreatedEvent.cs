using System.Web;
using Domain.Common.Abstractions;

namespace Application.Companies.Events
{
    internal class CompanyCreatedEvent : IDomainEvent
    {
        public required int UserId { get; set; }

        public bool RetryOnError => true;

        internal class Handler(
            IEmailService emailService,
            IDbContext dbContext,
            IIdentityService identityService,
            IStaticContentReader staticContentReader,
            IConfiguration configuration) : INotificationHandler<CompanyCreatedEvent>
        {
            public async Task Handle(CompanyCreatedEvent notification, CancellationToken cancellationToken)
            {
                var user = await dbContext.Users
                    .Where(x => x.Id == notification.UserId)
                    .Select(x => new
                    {
                        x.Email,
                    })
                    .FirstAsync(cancellationToken);

                var message = @$"Welcome to BIT HRMS, an account has been created for you. Please click the button to set your password.";
                var token = await identityService.GenerateAccountVerificationTokenAsync(notification.UserId);
                var link = $"{configuration.GetValue<string>("FrontEnd:Url")}/set-password/{notification.UserId}?token={HttpUtility.UrlEncode(token)}";

                var emailText = await staticContentReader.ReadContentAsync("email-template.html");

                emailText = emailText.Replace("[NAME]", user.Email);
                emailText = emailText.Replace("[MESSAGE]", message);
                emailText = emailText.Replace("[BUTTON TEXT]", "Get Started");
                emailText = emailText.Replace("[FROM]", "BIT HRMS");
                emailText = emailText.Replace("[LINK]", link);
                emailText = emailText.Replace("[FOOTER]", $"BIT HRMS ({DateTime.UtcNow.Year})");
                emailService.Send(user.Email!.ToValueObject<Email>(), "Account Created", emailText);
            }
        }
    }
}
