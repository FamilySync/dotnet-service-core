using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FamilySync.Core.Persistence.Models.Requests;

public interface ICreateRequestBase
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}

public class CreateRequestBase : ICreateRequestBase
{
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DefaultValue("SYSTEM"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CreatedBy { get; set; } = "SYSTEM";
}