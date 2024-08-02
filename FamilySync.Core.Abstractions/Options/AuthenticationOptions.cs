namespace FamilySync.Core.Abstractions.Options;

public class AuthenticationOptions
{
    public static string Section => "Config:Authentication";
    
    public string Secret { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
}