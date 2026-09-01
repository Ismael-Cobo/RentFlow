using Microsoft.EntityFrameworkCore;
using RentFlow.Modules.Fleet.Domain.Vehicle;
using RentFlow.Modules.Fleet.Infrastructure.Database;

namespace RentFlow.Modules.Fleet.Infrastructure.Vehicles;

internal sealed class VehicleRepository(FleetDbContext dbContext) : IVehicleRepository
{
    private readonly FleetDbContext _dbContext = dbContext;

    public async Task<Vehicle?> GetVehicleAsync(
        Guid vehicleId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Vehicles.SingleOrDefaultAsync(
            vehicle => vehicle.Id == vehicleId,
            cancellationToken);
    }

    public async Task<bool> ExistsByLicensePlateAsync(
        string licensePlate,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Vehicles.AnyAsync(
            vehicle => vehicle.LicensePlate == licensePlate,
            cancellationToken);
    }

    public async Task<IReadOnlyCollection<Vehicle>> GetVehiclesAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Vehicles
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Vehicle>> GetAvailableVehiclesAsync(
        VehicleCategory? category,
        CancellationToken cancellationToken)
    {
        IQueryable<Vehicle> query = _dbContext.Vehicles
            .AsNoTracking()
            .Where(vehicle => vehicle.Status == VehicleStatus.Available);

        if (category.HasValue)
        {
            query = query.Where(vehicle => vehicle.Category == category.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public void Insert(Vehicle vehicle)
    {
        _dbContext.Vehicles.Add(vehicle);
    }
}
