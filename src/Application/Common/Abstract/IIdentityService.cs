using System.Security.Claims;
using Domain.Companies;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Abstract;

public interface IIdentityService
{
    Task<bool> VerifyCredentialsAsync(Email email, string password);

    Task LogoutAsync(int userId);

    Task CreateUserAsync(User user, IdentityRole<int> role);

    Task<IList<Claim>> GetClaimsAsync(User user);

    Task SigninAsync(User user);

    Task<User?> GetUserAsync(Email email);

    Task<IdentityRole<int>> GetRoleAsync(RoleName roleName);

    Task AssignRoleAsync(User user, IdentityRole<int> role);

    Task<string> GenerateAccountVerificationTokenAsync(int userId);

    Task VerifyAccountAsync(int userId, string token, string password);

    Task ChangePasswordAsync(int userId, string currentPassword, string newPassword);

    Task<string> GeneratePasswordResetTokenAsync(int userId);

    Task ResetPasswordAsync(int userId, string token, string password);

    Task<bool> IsEmailConfirmedAsync(int userId);

    Task AddSuperAdminAsync(Email email, PhoneNumber phoneNumber, string password);
}
