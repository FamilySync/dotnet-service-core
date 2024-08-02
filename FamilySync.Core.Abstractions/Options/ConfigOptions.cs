namespace FamilySync.Core.Abstractions.Options;

public class ConfigOptions
{
    public static string Section => "Config";
    
    public ServiceOptions Service { get; set; } = new();
    public InclusionOptions Inclusion { get; set; } = new();
    public AuthenticationOptions Authentication { get; set; } = new();
    public CorsOptions Cors { get; set; }
}