namespace WebApp.Features.Timeline;

public partial class TravelPlanTimeline
{
    [Parameter] public IReadOnlyList<TravelPlan> TravelPlans { get; set; } = default!;
    [Parameter] public Func<TravelPlan, Task> OnEditTravelPlan { get; set; } = default!;

    public IReadOnlyList<TravelPlan> NonCompletedTravelPlans => 
        TravelPlans.Where(x => x.Type != TravelPlanType.Completed).ToList();
}