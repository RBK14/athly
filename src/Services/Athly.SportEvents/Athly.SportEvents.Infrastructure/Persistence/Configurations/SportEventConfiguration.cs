using Athly.SportEvents.Domain.SportEventAggregate;
using Athly.SportEvents.Domain.SportEventAggregate.Enums;
using Athly.SportEvents.Domain.SportEventAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetTopologySuite.Geometries;
namespace Athly.SportEvents.Infrastructure.Persistence.Configurations;

public class SportEventConfiguration : IEntityTypeConfiguration<SportEvent>
{
    public void Configure(EntityTypeBuilder<SportEvent> builder)
    {
        ConfigureSportEventsTable(builder);
    }

    private static void ConfigureSportEventsTable(EntityTypeBuilder<SportEvent> builder)
    {
        builder.ToTable("SportEvents");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => SportEventId.Of(value)
            )
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(e => e.ExternalId)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(e => e.ExternalId)
            .IsUnique();

        builder.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Sport)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.Date)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasConversion(
                status => status.ToString(),
                value => SportEventStatusExtensions.Parse(value))
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();

        builder.OwnsOne(e => e.EventCoordinates, cb =>
        {
            cb.Property(c => c.Longitude)
              .HasColumnName("Longitude")
              .IsRequired();

            cb.Property(c => c.Latitude)
              .HasColumnName("Latitude")
              .IsRequired();
        });

        builder.Property<Point>("Location")
            .HasColumnType("geography")
            .HasComputedColumnSql("geography::Point(Latitude, Longitude, 4326)", stored: true);

        builder.Property(e => e.VenueName)
            .HasMaxLength(150);

        builder.Property(e => e.City)
            .HasMaxLength(100);

        builder.Property(e => e.Country)
            .HasMaxLength(100);

        builder.Property(e => e.League)
           .HasMaxLength(100);

        builder.Property(e => e.Season)
            .HasMaxLength(20);

        builder.Property(e => e.Description)
            .HasMaxLength(2000);

        builder.Property(e => e.ImageUrl)
            .HasMaxLength(500);

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}