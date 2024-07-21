using FamilySync.Core.Authentication.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace FamilySync.Core.Authentication.Claims;

public class ClaimRequirement : IAuthorizationRequirement
{
    public string[] Claim { get; init; }
    public int Level { get; init; }

    public ClaimRequirement(string claim, ClaimLevel claimLevel)
    {
        Claim = claim.GetHierarchy().ToArray();
        Level = (int)claimLevel;
    }
}