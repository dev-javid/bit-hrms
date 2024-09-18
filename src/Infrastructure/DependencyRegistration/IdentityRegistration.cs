using System.Text;
using Infrastructure.EntityFramework;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.DependencyRegistration;

public static class IdentityRegistration
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>(IdentityService.TokenProviderName)
            .AddDefaultTokenProviders();

        JwtConfiguration jwtConfiguration = new();
        services.AddSingleton(jwtConfiguration);

        configuration.GetSection("Jwt").Bind(jwtConfiguration);
        var secretKey = Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            };
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
        });

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
