namespace FamilySync.Core.Authentication.Claims;

public class CustomClaim
{
    private static List<CustomClaim>? _definitions;

    public static List<CustomClaim> Definitions
    {
        get
        {
            return _definitions ??= ClaimDefinition.Build();
        }
    }

    public string Claim { get; init; }
    public string Description { get; set; }
    public string Name { get; set; }
    public string Policy { get; set; }
}