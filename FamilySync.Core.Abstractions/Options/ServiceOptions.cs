namespace FamilySync.Core.Abstractions.Options;

public class ServiceOptions
{
    public static string Section = "Config:Service";
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default;
    public string Route { get; set; } = default!;
    public bool IsDevelopment { get; set; } = false;
}