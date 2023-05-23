using Application.Commands;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WebApp.Pages;

public partial class TravelPlans
{
    [Inject] public IMediator Mediator { get; set; } = default!;
    [Inject] public IDialogService DialogService { get; set; } = default!;
    [Inject] public ISnackbar Snackbar { get; set; } = default!;

    private bool _loading = false;
    private IReadOnlyList<TravelPlan>? _travelPlans;

    public TravelPlans() {}

    protected override async Task OnInitializedAsync()
    {
        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        try
        {
            _loading = true;
            var query = new GetAllTravelPlansQuery();
            _travelPlans = await Mediator.Send(query);
        }
        finally
        {
            _loading = false;
        }
    }

    async Task CreateTravelPlan()
    {
        await CreateOrUpdateTravelPlan(TravelPlan.Empty);
    }

    async Task UpdateTravelPlan(TravelPlan travelPlan)
    {
        await CreateOrUpdateTravelPlan(travelPlan);
    }

    async Task CreateOrUpdateTravelPlan(TravelPlan travelPlan)
    {
        var parameters = new DialogParameters { ["travelPlan"] = travelPlan };

        var dialog = await DialogService.ShowAsync<CreateTravelPlanDialog>("Create Travel Plan", parameters);
        var result = await dialog.Result;

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
