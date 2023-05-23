using Domain.Enums;

namespace Domain.Entities;

public class TravelPlan : BaseEntity
{
    public TravelPlan(string name)
    {
        Name = name;
        Type = TravelPlanType.Suggestion;
        ThingsToDo = new List<TodoItem>();
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public TravelPlanType Type { get; private set; }
    public int? Budget { get; private set; }
    public List<TodoItem> ThingsToDo { get; private set; }
    public Travel? TravelFrom { get; private set; }
    public Travel? TravelTo { get; private set; }
    public bool IsDeleted { get; private set; }

    public bool IsNew => Id == Guid.Empty;

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeDescription(string? description)
    {
        Description = description;
    }

    public void ChangeType(TravelPlanType newType)
    {
        Type = newType;
    }

    public void ChangeBudget(int? budget)
    {
        Budget = budget;
    }

    public void AddThingToDo(TodoItem item)
    {
        ThingsToDo.Add(item);
    }

    public void DeleteThingToDo(Guid todoId)
    {
        ThingsToDo.RemoveAll(t => t.Id == todoId);
    }

    public void SetDeleted()
    {
        IsDeleted = true;
    }

    public static TravelPlan Empty => new(string.Empty);
}