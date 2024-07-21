namespace FamilySync.Core.Authentication.Claims;

public static class ClaimDefinition
{
    public static List<CustomClaim> Build()
    {
        List<CustomClaim> claims = new();

        claims.Add(new()
        {
            Name = "FamilySync",
            Description = "Root claim.",
            Claim = "fs",
            Policy = "familysync"
        });

        return claims;
    }
}