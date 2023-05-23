using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class DeleteTravelPlanCommand : IRequest
{
    public DeleteTravelPlanCommand(Guid travelPlanId)
    {
        TravelPlanId = travelPlanId;
    }

    public Guid TravelPlanId { get; init; }
}

public class DeleteTravelPlanCommandHandler : IRequestHandler<DeleteTravelPlanCommand>
{
    private readonly ITravelPlanRepository _repository;

    public DeleteTravelPlanCommandHandler(ITravelPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteTravelPlanCommand command, CancellationToken cancellationToken)
    {
        var travelPlan = await _repository.GetAsync(command.TravelPlanId, cancellationToken);
        travelPlan.SetDeleted();
        await _repository.UpdateAsync(travelPlan, cancellationToken);
    }
}