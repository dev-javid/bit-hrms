using System.Web;
using Application.Common.MediatR;

namespace Application.Auth.Commands;

public class ForgotPasswordCommand : IUpdateCommand
{
    public string Email { get; init; } = null!;

    public class Validator : AbstractValidator<ForgotPasswordCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();

            //TO DO: Add validator to check email exists in system using identityService
        }
    }

    internal class Handler(
        IIdentityService identityService,
        IDbContext dbContext,
        IEmailService emailService,
        IStaticContentReader staticContentReader,
        IConfiguration configuration) : IUpdateCommandHandler<ForgotPasswordCommand>
    {
        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var email = Domain.Common.ValueObjects.Email.From(request.Email);
            var user = await dbContext.Users
                .Where(x => x.Email == email.Value)
                .Select(x => new
                {
                    x.Id,
                    x.Email,
                    Name = x.Employee != null ? x.Employee.FullName : "there",
                })
                .FirstAsync(cancellationToken);

            var token = await identityService.GeneratePasswordResetTokenAsync(user!.Id);
            var link = $"{configuration.GetValue<string>("FrontEnd:Url")}/reset-password/{user!.Id}?token={HttpUtility.UrlEncode(token)}";

            var emailText = await staticContentReader.ReadContentAsync("email-templates/forgot-password.html");
            emailText = emailText.Replace("[User Name]", user.Name);
            emailText = emailText.Replace("[Year]", DateTime.Now.Year.ToString());
            emailText = emailText.Replace("[Reset Password Link]", link);
            emailService.Send(user.Email!.ToValueObject<Email>(), "Reset Your Password", emailText);

            return true;
        }
    }
}
