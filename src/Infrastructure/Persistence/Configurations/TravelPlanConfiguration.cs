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

        builder.OwnsOne(tp => tp.TravelHome);
        builder.OwnsOne(tp => tp.TravelTo);
    }
}
