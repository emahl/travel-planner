using Domain.Entities;

namespace Application.Interfaces;

public interface ITravelPlanRepository
{
    Task<IReadOnlyList<TravelPlan>> GetAllAsync(CancellationToken cancellationToken);
    Task<TravelPlan> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<TravelPlan> CreateAsync(TravelPlan travelPlan, CancellationToken cancellationToken);
    Task UpdateAsync(TravelPlan travelPlan, CancellationToken cancellationToken);
}
