using Microsoft.AspNetCore.Components.Web;

namespace WebApp.Features.Timeline;

public partial class TravelPlanTimelineItem
{
    [Parameter] public TravelPlan TravelPlan { get; set; } = default!;
    [Parameter] public int Index { get; set; } = default!;
    [Parameter] public Func<TravelPlan, Task> OnEditTravelPlan { get; set; } = default!;

    private bool _isHovering;

    async Task OnClickEdit()
    {
        await OnEditTravelPlan(TravelPlan);
    }

    public string TravelTypeIcon => TravelPlan.TravelTo.Type switch
    {
        TravelType.Airplane => Icons.Material.Filled.AirplanemodeActive,
        TravelType.Train => Icons.Material.Filled.Train,
        TravelType.Bus => Icons.Material.Filled.DirectionsBus,
        TravelType.Boat => Icons.Material.Filled.DirectionsBoat,
        TravelType.Car => Icons.Material.Filled.DirectionsCar,
        TravelType.Unknown or _ => string.Empty
    };

    public Color ColorBasedOnIndex => (Index % 4) switch
    {
        1 => Color.Primary,
        2 => Color.Secondary,
        3 => Color.Dark,
        0 or _ => Color.Success
    };

    void OnMouseEnter(MouseEventArgs _)
    {
        _isHovering = true;
        StateHasChanged();
    }

    void OnMouseLeave(MouseEventArgs _)
    {
        _isHovering = false;
        StateHasChanged();
    }
}
