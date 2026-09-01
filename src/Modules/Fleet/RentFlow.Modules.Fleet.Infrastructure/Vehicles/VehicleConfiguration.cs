using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentFlow.Modules.Fleet.Domain.Vehicle;
using RentFlow.Modules.Fleet.Infrastructure.Database;

namespace RentFlow.Modules.Fleet.Infrastructure.Vehicles;

internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles", Schemas.Vehicles);

        builder.HasKey(vehicle => vehicle.Id);

        builder.Property(vehicle => vehicle.Id)
            .ValueGeneratedNever();

        builder.Property(vehicle => vehicle.Brand)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(vehicle => vehicle.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(vehicle => vehicle.LicensePlate)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(vehicle => vehicle.LicensePlate)
            .IsUnique();

        builder.Property(vehicle => vehicle.Category)
            .IsRequired();

        builder.Property(vehicle => vehicle.DailyPrice)
            .IsRequired();

        builder.Property(vehicle => vehicle.Status)
            .IsRequired();

        builder.Property(vehicle => vehicle.CreatedAt)
            .IsRequired();
    }
}
