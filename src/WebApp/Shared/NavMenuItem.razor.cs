namespace WebApp.Shared;

public partial class NavMenuItem
{
    [Parameter] public string? Title { get; set; }
    [Parameter] public string? Icon { get; set; }
    [Parameter] public string? Route { get; set; }
}
