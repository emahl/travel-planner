using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class CreateTravelPlanCommand : IRequest<TravelPlan>
{
    public CreateTravelPlanCommand(TravelPlan travelPlanToCreate)
    {
        TravelPlanToCreate = travelPlanToCreate;
    }

    public TravelPlan TravelPlanToCreate { get; init; }
}

public class CreateTravelPlanCommandHandler : IRequestHandler<CreateTravelPlanCommand, TravelPlan>
{
    private readonly ITravelPlanRepository _repository;

    public CreateTravelPlanCommandHandler(ITravelPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<TravelPlan> Handle(CreateTravelPlanCommand command, CancellationToken cancellationToken)
    {
        var travelPlan = await _repository.CreateAsync(command.TravelPlanToCreate, cancellationToken);
        return travelPlan;
    }
}