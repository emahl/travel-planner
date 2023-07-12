using WebApp.Features.Manage;

namespace WebApp.Features.Shared;

public static class CreateEditTravelPlanDialogUtils
{
    public static async Task<DialogResult> OpenDialogAsync(
        IDialogService dialogService, TravelPlan travelPlan)
    {
        var parameters = new DialogParameters { ["travelPlan"] = travelPlan };
        var dialogOptions = new DialogOptions() { CloseOnEscapeKey = true, DisableBackdropClick = true };

        var dialog = await dialogService.ShowAsync<CreateEditTravelPlanDialog>(
            "Create Travel Plan", parameters, dialogOptions);
        var result = await dialog.Result;

        return result;
    }
}
