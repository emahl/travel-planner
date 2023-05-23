using Common;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDateTimeService _dateTimeService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeService dateTimeService) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        _httpContextAccessor = httpContextAccessor;
        _dateTimeService = dateTimeService;
    }

    public virtual DbSet<TravelPlan> TravelPlans { get; set; }
    public virtual DbSet<Travel> Travels { get; set; }
    public virtual DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<TravelPlan>().HasQueryFilter(p => !p.IsDeleted);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var insertedEntries = ChangeTracker.Entries()
                   .Where(x => x.State == EntityState.Added)
                   .Select(x => x.Entity);

        foreach (var insertedEntity in insertedEntries.OfType<BaseEntity>())
        {
            insertedEntity.Id = Guid.NewGuid();
            insertedEntity.CreatedDate = _dateTimeService.Now;
            insertedEntity.CreatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? string.Empty;
        }

        var modifiedEntries = ChangeTracker.Entries()
                   .Where(x => x.State == EntityState.Modified)
                   .Select(x => x.Entity);

        foreach (var modifiedEntry in modifiedEntries.OfType<BaseEntity>())
        {
            modifiedEntry.UpdatedDate = _dateTimeService.Now;
            modifiedEntry.UpdatedBy = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? string.Empty;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}