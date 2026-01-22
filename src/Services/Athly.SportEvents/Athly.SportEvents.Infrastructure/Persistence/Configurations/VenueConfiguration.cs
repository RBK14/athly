using Athly.SportEvents.Domain.Cities;
using Athly.SportEvents.Domain.Venues;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Athly.SportEvents.Infrastructure.Persistence.Configurations;

public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.ToTable("Venues");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id)
            .HasConversion(id => id.Value, value => VenueId.Of(value))
            .ValueGeneratedNever();

        builder.Property(v => v.ExternalId).HasMaxLength(100).IsRequired();
        builder.HasIndex(v => v.ExternalId).IsUnique();

        builder.Property(v => v.Name).HasMaxLength(150).IsRequired();

        builder.Property(v => v.CityId)
            .HasConversion(id => id.Value, value => CityId.Of(value))
            .IsRequired();

        builder.HasOne<City>()
            .WithMany()
            .HasForeignKey(v => v.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsOne(v => v.Coordinates, cb =>
        {
            cb.Property(c => c.Latitude).HasColumnName("Latitude").IsRequired();
            cb.Property(c => c.Longitude).HasColumnName("Longitude").IsRequired();
        });

        builder.Property(v => v.CreatedAt).IsRequired();
        builder.Property(v => v.UpdatedAt);
    }
}