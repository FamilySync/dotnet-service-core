namespace FamilySync.Core.Example.Models.Requests;

public class CreateTokenRequest
{
    public Guid ID { get; set; }
    public string Email { get; set; }
    public IDictionary<string, object> Claims { get; set; }
}