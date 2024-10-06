using Application.Common.MediatR;
using Domain.Identity;

namespace Application.Users.Commands;

public class AddUserCommand : IAddCommand<int>
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Role { get; set; } = null!;

    [JsonIgnore]
    internal bool UseTransaction { get; init; } = true;

    internal class Handler(IIdentityService identityService, IDbContext dbContext) : IAddCommandHandler<AddUserCommand, int>
    {
        public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.UseTransaction)
            {
                return await CreateUserAsync(request, cancellationToken);
            }
            else
            {
                using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = await CreateUserAsync(request, cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return userId;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }

        private async Task<int> CreateUserAsync(AddUserCommand request, CancellationToken cancellationToken)
        {
            var role = await identityService.GetRoleAsync(request.Role.AsEnum<RoleName>());
            var user = User.Create(request.Email.ToValueObject<Email>(), request.PhoneNumber.ToValueObject<PhoneNumber>());

            var claims = new Dictionary<string, string>
            {
                { "Name", request.Name },
            };

            user.AddClaims(claims);
            await identityService.CreateUserAsync(user, role);

            await dbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
    }
}
