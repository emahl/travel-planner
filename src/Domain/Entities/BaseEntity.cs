namespace Domain.Entities;

public abstract class BaseEntity : AuditableEntity
{
    public Guid Id { get; set; }
}
