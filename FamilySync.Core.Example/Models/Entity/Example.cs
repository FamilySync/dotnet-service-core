using FamilySync.Core.Persistence.Models;

namespace FamilySync.Core.Example.Models.Entity;

public class Example : EntityBase<Guid>
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}