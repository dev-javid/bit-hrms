using System.Security.Claims;
using Domain.Companies;

namespace Infrastructure.Services;

internal class IdentityService(
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager,
        SignInManager<User> signInManager) : IIdentityService
{
    public const string TokenProviderName = "HRMS.Pro";
    private const string RefreshTokenName = "RefreshToken";

    public async Task LogoutAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        await userManager.RemoveAuthenticationTokenAsync(user!, TokenProviderName, RefreshTokenName);
        var result = await userManager.UpdateSecurityStampAsync(user!);
        VerifyResult(result);
    }

    public async Task<bool> VerifyCredentialsAsync(Email email, string password)
    {
        var user = await userManager.FindByEmailAsync(email.Value);
        return user != null && await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IList<Claim>> GetClaimsAsync(User user)
    {
        return await userManager.GetClaimsAsync(user);
    }

    public async Task SigninAsync(User user)
    {
        await signInManager.SignInAsync(user, false);
    }

    public async Task CreateUserAsync(User user, IdentityRole<int> role)
    {
        var result = await userManager.CreateAsync(user);
        VerifyResult(result);
        await AssignRoleAsync(user, role);
    }

    public async Task<User?> GetUserAsync(Email email)
    {
        return await userManager.FindByEmailAsync(email.Value);
    }

    public async Task<IdentityRole<int>> GetRoleAsync(RoleName roleName)
    {
        return await roleManager.FindByNameAsync(roleName.ToString())
            ?? throw CustomException.WithBadRequest($"Role {roleName} not found.");
    }

    public async Task AssignRoleAsync(User user, IdentityRole<int> role)
    {
        foreach (var roleName in (RoleName[])Enum.GetValues(typeof(RoleName)))
        {
            if (await userManager.IsInRoleAsync(user, roleName.ToString()))
            {
                var result = await userManager.RemoveFromRoleAsync(user, roleName.ToString());
                VerifyResult(result);
            }
        }

        var result2 = await userManager.AddToRoleAsync(user, role.Name!);
        VerifyResult(result2);
    }

    public async Task<string> GenerateAccountVerificationTokenAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return await userManager.GenerateEmailConfirmationTokenAsync(user!);
    }

    public async Task VerifyAccountAsync(int userId, string token, string password)
    {
        var user = await userManager.Users.FirstAsync(u => u.Id == userId);
        var result = await userManager.ConfirmEmailAsync(user, token);
        VerifyResult(result);
        result = await userManager.AddPasswordAsync(user, password);
        VerifyResult(result);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return await userManager.GeneratePasswordResetTokenAsync(user!);
    }

    public async Task ResetPasswordAsync(int userId, string token, string password)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        var result = await userManager.ResetPasswordAsync(user!, token, password);
        VerifyResult(result);
    }

    public async Task ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        var result = await userManager.ChangePasswordAsync(user!, currentPassword, newPassword);
        VerifyResult(result);
    }

    public async Task<bool> IsEmailConfirmedAsync(int userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return await userManager.IsEmailConfirmedAsync(user!);
    }

    public async Task AddSuperAdminAsync(Email email, PhoneNumber phoneNumber, string password)
    {
        foreach (var roleName in (RoleName[])Enum.GetValues(typeof(RoleName)))
        {
            var newRole = new IdentityRole<int>(roleName.ToString());
            var result = await roleManager.CreateAsync(newRole);
            VerifyResult(result);
        }

        User user = User.Create(email, phoneNumber);
        var role = await GetRoleAsync(RoleName.SuperAdmin);
        await CreateUserAsync(user, role);
        var token = await GenerateAccountVerificationTokenAsync(user.Id);
        await VerifyAccountAsync(user.Id, token, password);
    }

    private static void VerifyResult(IdentityResult result)
    {
        if (!result.Succeeded)
        {
            throw CustomException.WithInternalServer(string.Join(" | ", result.Errors.Select(x => x.Description)));
        }
    }
}
