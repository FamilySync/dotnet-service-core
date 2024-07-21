using System.Text;
using FamilySync.Core.Abstractions.Options;
using FamilySync.Core.Authentication.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FamilySync.Core.Authentication.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, AuthenticationOptions authOptions) 
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicies();
        });
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;

                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Secret)),
                    ClockSkew = TimeSpan.FromSeconds(10)
                };
            });

        services.AddSingleton<IAuthorizationHandler, ClaimAccessHandler>();

        return services;
    }
}