namespace Infrastructure.DependencyRegistration;

public class JwtConfiguration
{
    public string SecretKey { get; set; } = null!;

    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public int AccessTokenLifetimeInMinutes { get; set; }

    public int RefreshTokenLifetimeInMinutes { get; set; }
}
