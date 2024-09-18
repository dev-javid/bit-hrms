using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Infrastructure.Services
{
    internal class CustomClaimsTransformation(IDbContext dbContext) : IClaimsTransformation
    {
        public const string CompanyClaim = "companyId";

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (!principal.HasClaim(claim => claim.Type == CompanyClaim))
            {
                var userId = int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

                if (userId > 0)
                {
                    var user = await dbContext
                        .Users
                        .IgnoreQueryFilters()
                        .FirstAsync(x => x.Id == userId);

                    ClaimsIdentity claimsIdentity = new();
                    claimsIdentity.AddClaim(new Claim(CompanyClaim, user!.CompanyId.ToString()));
                }
            }

            return principal;
        }
    }
}
