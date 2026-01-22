using Athly.SportEvents.Domain.Cities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Athly.SportEvents.Infrastructure.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(id => id.Value, value => CityId.Of(value))
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(c => new { c.Name, c.Country });

        builder.Property(c => c.Country)
            .HasMaxLength(100)
            .IsRequired();

        builder.OwnsOne(c => c.Coordinates, cb =>
        {
            cb.Property(c => c.Latitude).HasColumnName("Latitude").IsRequired();
            cb.Property(c => c.Longitude).HasColumnName("Longitude").IsRequired();
        });

        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt);
    }
}