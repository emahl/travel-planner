using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public class TravelPlanConfiguration : IEntityTypeConfiguration<TravelPlan>
{
    public void Configure(EntityTypeBuilder<TravelPlan> builder)
    {
        builder
            .HasKey(tp => tp.Id);

        builder.Property(tp => tp.Type).HasConversion<int>();

        builder
            .HasMany(tp => tp.ThingsToDo)
            .WithOne()
            .IsRequired();

        builder.Navigation(tp => tp.TravelFrom).AutoInclude();
        
        builder.Navigation(tp => tp.TravelTo).AutoInclude();
        builder.Navigation(tp => tp.ThingsToDo).AutoInclude();
    }
}
