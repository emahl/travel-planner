using Common;
using Domain.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly IDateTimeService _dateTimeService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        AuthenticationStateProvider authStateProvider,
        IDateTimeService dateTimeService) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        _authStateProvider = authStateProvider;
        _dateTimeService = dateTimeService;
    }

    public virtual DbSet<TravelPlan> TravelPlans { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<TravelPlan>().HasQueryFilter(p => !p.IsDeleted);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var insertedEntries = ChangeTracker.Entries()
                   .Where(x => x.State == EntityState.Added)
                   .Select(x => x.Entity);

        foreach (var insertedEntity in insertedEntries.OfType<BaseEntity>())
        {
            insertedEntity.Id = Guid.NewGuid();
        }

        foreach (var insertedEntity in insertedEntries.OfType<AuditableEntity>())
        {
            insertedEntity.CreatedDate = _dateTimeService.Now;
            insertedEntity.CreatedBy = await GetLoggedInUserEmailAsync();
        }

        var modifiedEntries = ChangeTracker.Entries()
                   .Where(x => x.State == EntityState.Modified)
                   .Select(x => x.Entity);

        foreach (var modifiedEntry in modifiedEntries.OfType<AuditableEntity>())
        {
            modifiedEntry.UpdatedDate = _dateTimeService.Now;
            modifiedEntry.UpdatedBy = await GetLoggedInUserEmailAsync();
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task<string> GetLoggedInUserEmailAsync()
    {
        var emailType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var claims = authState?.User?.Claims;

        return claims?.FirstOrDefault(c => c.Type == emailType)?.Value ?? "<Unknown>";
    }
}