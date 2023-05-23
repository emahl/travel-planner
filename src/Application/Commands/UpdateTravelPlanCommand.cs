using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class UpdateTravelPlanCommand : IRequest
{
    public UpdateTravelPlanCommand(TravelPlan travelPlanToUpdate)
    {
        TravelPlanToUpdate = travelPlanToUpdate;
    }

    public TravelPlan TravelPlanToUpdate { get; init; }
}

public class UpdateTravelPlanCommandHandler : IRequestHandler<UpdateTravelPlanCommand>
{
    private readonly ITravelPlanRepository _repository;

    public UpdateTravelPlanCommandHandler(ITravelPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateTravelPlanCommand command, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(command.TravelPlanToUpdate, cancellationToken);
    }
}