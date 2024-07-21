using FamilySync.Core.Authentication.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace FamilySync.Core.Authentication.Extensions;

public static class AuthorizationOptionsExtensions
{
    public static void AddPolicies(this AuthorizationOptions options)
    {
        var levels = Enum.GetValues<ClaimLevel>();

        foreach (var definition in CustomClaim.Definitions)
        {
            if (string.IsNullOrEmpty(definition.Policy))
            {
                continue;
            }

            foreach (var level in levels)
            {
                options.AddPolicy($"{definition.Policy}:{Enum.GetName(level)}", config =>
                {
                    config.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    config.RequireAuthenticatedUser();
                    config.AddRequirements(new ClaimRequirement(definition.Claim, level));
                });
            }
        }

        options.DefaultPolicy = new AuthorizationPolicyBuilder([JwtBearerDefaults.AuthenticationScheme])
            .RequireAuthenticatedUser()
            .Build();
    }
}