using Microsoft.EntityFrameworkCore;
using RentFlow.Modules.Fleet.Application.Abstractions.Data;
using RentFlow.Modules.Fleet.Domain.Vehicle;
using RentFlow.Modules.Fleet.Infrastructure.Vehicles;

namespace RentFlow.Modules.Fleet.Infrastructure.Database;

public sealed class FleetDbContext(DbContextOptions<FleetDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Vehicles);
        modelBuilder.ApplyConfiguration(new VehicleConfiguration());
    }
}
