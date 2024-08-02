namespace FamilySync.Core.Abstractions.Options;

public class CorsOptions
{
    public static string Section => "Config:Cors";
    
    public string Name { get; set; } = default!;
    public string[] Origins { get; set; } = default!;
}