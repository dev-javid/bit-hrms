using System.Web;
using Domain.Common.Abstractions;

namespace Application.Employees.Events
{
    internal class EmployeeCreatedEvent : IDomainEvent
    {
        public required int EmployeeId { get; set; }

        public bool RetryOnError => true;

        internal class Handler(
            IEmailService emailService,
            IDbContext dbContext,
            IIdentityService identityService,
            IStaticContentReader staticContentReader,
            IConfiguration configuration) : INotificationHandler<EmployeeCreatedEvent>
        {
            public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
            {
                var user = await dbContext.Employees
                    .Where(x => x.Id == notification.EmployeeId)
                    .Select(x => new
                    {
                        x.UserId,
                        x.User.Email,
                        CompanyName = x.Company.Name,
                        CompanyAddress = x.Company.Address,
                        Name = x.FullName
                    })
                    .FirstAsync(cancellationToken);

                var message = @$"Welcome to ${user.CompanyName}, an account has been created for you. Please click the button to set your password.";
                var token = await identityService.GenerateAccountVerificationTokenAsync(user.UserId);
                var link = $"{configuration.GetValue<string>("FrontEnd:Url")}/set-password/{user!.UserId}?token={HttpUtility.UrlEncode(token)}";

                var emailText = await staticContentReader.ReadContentAsync("email-template.html");

                emailText = emailText.Replace("[NAME]", user.Name);
                emailText = emailText.Replace("[MESSAGE]", message);
                emailText = emailText.Replace("[BUTTON TEXT]", "Get Started");
                emailText = emailText.Replace("[FROM]", user.CompanyName);
                emailText = emailText.Replace("[LINK]", link);
                emailText = emailText.Replace("[FOOTER]", user.CompanyAddress);
                emailService.Send(user.Email!.ToValueObject<Email>(), "Account Created", emailText);
            }
        }
    }
}
