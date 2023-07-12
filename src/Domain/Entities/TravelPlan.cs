using Domain.Enums;
using System.Globalization;

namespace Domain.Entities;

public class TravelPlan : BaseEntity
{
    public TravelPlan(string name)
    {
        Name = name;
        Type = TravelPlanType.Suggestion;
        TravelTo = Travel.Empty;
        TravelHome = Travel.Empty;
    }

    public string Name { get; private set; }
    public string? Description { get; private set; }
    public TravelPlanType Type { get; private set; }
    public int? Budget { get; private set; }
    public Travel TravelTo { get; private set; }
    public Travel TravelHome { get; private set; }
    public bool IsDeleted { get; private set; }

    private CultureInfo _culture = CultureInfo.CreateSpecificCulture("en-US");

    public string TravelDateStart => TravelTo.DepartureDate != null ?
        TravelTo.DepartureDate.Value.ToString("dddd, d MMMM, yyyy", _culture) : "?";

    public string TravelDateSummary => TravelTo.DepartureDate != null && TravelHome.ArrivalDate != null ?
        $"{TravelTo.DepartureDate.Value.ToString("d MMMM yyyy", _culture)} - {TravelHome.ArrivalDate.Value.ToString("d MMMM yyyy", _culture)}" :
        string.Empty;

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

    public void SetDeleted()
    {
        IsDeleted = true;
    }

    public static TravelPlan Empty => new(string.Empty);
}