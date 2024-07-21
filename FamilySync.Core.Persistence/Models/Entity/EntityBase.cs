namespace FamilySync.Core.Persistence.Models;

public interface IEntityBase
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}

public interface IEntityBase<T> : IEntityBase
{
    public T ID { get; set; }
}

public abstract class EntityBase : IEntityBase
{
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}

public abstract class EntityBase<T> : EntityBase, IEntityBase<T>
{
    public T ID { get; set; }
}