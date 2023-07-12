using WebApp.Features.Shared;

namespace WebApp.Features.Manage;

public partial class ManageTravelPlansPage
{
    [Inject] public IMediator Mediator { get; set; } = default!;
    [Inject] public IDialogService DialogService { get; set; } = default!;
    [Inject] public ISnackbar Snackbar { get; set; } = default!;

    private IReadOnlyList<TravelPlan>? _travelPlans;
    private bool _showCompleted;
    
    public bool Loading = false;
    public IReadOnlyList<TravelPlan>? FilteredTravelPlans;

    public bool ShowCompleted
    {
        get => _showCompleted;
        set
        {
            _showCompleted = value;
            FilterTravelPlans();
        }
    }

    public ManageTravelPlansPage() { }

    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            Loading = true;
            var query = new GetAllTravelPlansQuery();
            _travelPlans = await Mediator.Send(query);
            FilterTravelPlans();
        }
        finally
        {
            Loading = false;
        }
    }

    private void FilterTravelPlans()
    {
        FilteredTravelPlans = _travelPlans?
            .Where(tp => _showCompleted || tp.Type != TravelPlanType.Completed)?
            .ToList();
    }

    async Task CreateTravelPlan()
    {
        await CreateOrEditTravelPlan(TravelPlan.Empty);
    }

    async Task UpdateTravelPlan(TravelPlan travelPlan)
    {
        await CreateOrEditTravelPlan(travelPlan);
    }

    async Task CreateOrEditTravelPlan(TravelPlan travelPlan)
    {
        var result = await CreateEditTravelPlanDialogUtils
            .OpenDialogAsync(DialogService, travelPlan);
        
        if (result.Canceled) return;
        await LoadDataAsync();
    }

    async Task DeleteTravelPlan(Guid travelPlanId, string travelPlanName)
    {
        bool? result = await DialogService.ShowMessageBox(
            "Delete",
            $"Are you sure you want to delete the travel plan '{travelPlanName}'?",
            yesText: "Yes", cancelText: "Cancel");

        if (result == null) return;

        await Mediator.Send(new DeleteTravelPlanCommand(travelPlanId));
        Snackbar.Add("Travel plan deleted", Severity.Success);

        await LoadDataAsync();
    }
}
