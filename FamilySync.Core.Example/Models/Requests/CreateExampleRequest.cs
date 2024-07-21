using FamilySync.Core.Persistence.Models.Requests;

namespace FamilySync.Core.Example.Models.Requests;

public class CreateExampleRequest : CreateRequestBase
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}