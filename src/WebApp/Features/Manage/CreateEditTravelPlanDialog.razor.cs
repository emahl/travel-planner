namespace WebApp.Features.Manage;

public partial class CreateEditTravelPlanDialog
{
    [Inject] public IMediator Mediator { get; set; } = default!;
    [Inject] public ISnackbar Snackbar { get; set; } = default!;

    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter] public TravelPlan TravelPlan { get; set; } = default!;

    public bool IsSaveDisabled => string.IsNullOrEmpty(TravelPlan.Name);

    public string DialogTitle => TravelPlan.IsNew ? "Create new travel plan" : "Update travel plan";

    public string TravelToPanelTitle => $"Travel to: {TravelPlan.TravelTo.ShortTitle}";

    public string TravelHomePanelTitle => $"Travel home: {TravelPlan.TravelHome.ShortTitle}";

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private async Task CreateOrUpdate()
    {
        if (TravelPlan.IsNew)
        {
            var createdTravelPlan = await Mediator.Send(new CreateTravelPlanCommand(TravelPlan));
            Snackbar.Add("Travel plan created", Severity.Success);
            MudDialog.Close(DialogResult.Ok(createdTravelPlan.Id));
        }
        else
        {
            await Mediator.Send(new UpdateTravelPlanCommand(TravelPlan));
            Snackbar.Add("Travel plan updated", Severity.Success);
            MudDialog.Close(DialogResult.Ok(TravelPlan.Id));
        }
    }
}
