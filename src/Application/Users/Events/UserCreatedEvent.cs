using System.Web;
using Domain.Common.Abstractions;

namespace Application.Users.Events
{
    internal class UserCreatedEvent : IDomainEvent
    {
        public int UserId { get; set; }

        public bool RetryOnError => true;

        internal class Handler(
            IEmailService emailService,
            IDbContext dbContext,
            IIdentityService identityService,
            IStaticContentReader staticContentReader,
            IConfiguration configuration) : INotificationHandler<UserCreatedEvent>
        {
            public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
            {
                var user = await dbContext.Users
                    .Where(x => x.Id == notification.UserId)
                    .Select(x => new
                    {
                        x.Id,
                        x.Email,
                        CompanyName = x.Company.Name,
                        Name = x.Employee != null ? x.Employee.FullName : "there",
                    })
                    .FirstAsync(cancellationToken);

                var token = await identityService.GenerateAccountVerificationTokenAsync(notification.UserId);
                var link = $"{configuration.GetValue<string>("FrontEnd:Url")}/set-password/{user!.Id}?token={HttpUtility.UrlEncode(token)}";

                var emailText = await staticContentReader.ReadContentAsync("email-templates/account-created.html");
                emailText = emailText.Replace("[User Name]", user.Name);
                emailText = emailText.Replace("[Company Name]", user.CompanyName);
                emailText = emailText.Replace("[Year]", DateTime.Now.Year.ToString());
                emailText = emailText.Replace("[Confirmation Link]", link);
                emailService.Send(user.Email!.ToValueObject<Email>(), "Account Created", emailText);
            }
        }
    }
}
