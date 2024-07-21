using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FamilySync.Core.Persistence.Models.Requests;

public interface IUpdateRequestBase
{
    public DateTime UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}

public class UpdateRequestBase : IUpdateRequestBase
{
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [DefaultValue("SYSTEM")]
    public string? UpdatedBy { get; set; }
}