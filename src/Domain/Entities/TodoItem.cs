namespace Domain.Entities;

public class TodoItem : BaseEntity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime? Date { get; private set; }
    public bool IsDecided { get; private set; }
    public int Order { get; private set; }
}
