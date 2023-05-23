using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class TravelPlanRepository : ITravelPlanRepository
{
    private readonly ApplicationDbContext _context;

    public TravelPlanRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TravelPlan>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.TravelPlans.ToListAsync(cancellationToken);
    }

    public async Task<TravelPlan> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var travelPlan = await _context.TravelPlans.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (travelPlan is null)
        {
            throw new NotFoundException(nameof(TravelPlan), id);
        }

        return travelPlan;
    }

    public async Task<TravelPlan> CreateAsync(TravelPlan travelPlan, CancellationToken cancellationToken)
    {
        _context.TravelPlans.Add(travelPlan);
        await _context.SaveChangesAsync(cancellationToken);
        return travelPlan;
    }

    public async Task UpdateAsync(TravelPlan travelPlan, CancellationToken cancellationToken)
    {
        _context.Update(travelPlan);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
