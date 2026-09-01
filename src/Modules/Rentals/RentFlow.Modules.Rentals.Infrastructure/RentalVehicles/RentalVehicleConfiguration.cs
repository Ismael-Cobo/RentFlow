using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentFlow.Modules.Rentals.Domain.RentalVehicle;
using RentFlow.Modules.Rentals.Infrastructure.Database;

namespace RentFlow.Modules.Rentals.Infrastructure.RentalVehicles;

internal sealed class RentalVehicleConfiguration : IEntityTypeConfiguration<RentalVehicle>
{
    public void Configure(EntityTypeBuilder<RentalVehicle> builder)
    {
        builder.ToTable("rental_vehicles", Schemas.Rentals);

        builder.HasKey(vehicle => vehicle.Id);

        builder.Property(vehicle => vehicle.Id)
            .ValueGeneratedNever();

        builder.Property(vehicle => vehicle.DailyPrice)
            .IsRequired();

        builder.Property(vehicle => vehicle.IsAvailable)
            .IsRequired();

        builder.Ignore(vehicle => vehicle.DomainEvents);
    }
}
