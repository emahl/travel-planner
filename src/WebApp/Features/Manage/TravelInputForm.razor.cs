namespace WebApp.Features.Manage;

public partial class TravelInputForm
{
    [Parameter] public Travel Travel { get; set; } = default!;

    public CultureInfo DateCultureInfo = CultureInfo.CreateSpecificCulture("sv-SE");
}
