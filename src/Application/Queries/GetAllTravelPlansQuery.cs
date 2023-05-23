using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllTravelPlansQuery : IRequest<IReadOnlyList<TravelPlan>>
{
}

public class GetAllTravelPlansQueryHandler : IRequestHandler<GetAllTravelPlansQuery, IReadOnlyList<TravelPlan>>
{
    private readonly ITravelPlanRepository _repository;

    public GetAllTravelPlansQueryHandler(ITravelPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<TravelPlan>> Handle(GetAllTravelPlansQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    } 
}