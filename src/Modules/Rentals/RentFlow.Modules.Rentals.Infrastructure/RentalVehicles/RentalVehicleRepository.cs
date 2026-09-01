using Microsoft.EntityFrameworkCore;
using RentFlow.Modules.Rentals.Domain.RentalVehicle;
using RentFlow.Modules.Rentals.Infrastructure.Database;

namespace RentFlow.Modules.Rentals.Infrastructure.RentalVehicles;

internal sealed class RentalVehicleRepository(RentalsDbContext dbContext)
    : IRentalVehicleRepository
{
    private readonly RentalsDbContext _dbContext = dbContext;

    public async Task<RentalVehicle?> GetRentalVehicleAsync(
        Guid vehicleId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.RentalVehicles
            .AsNoTracking()
            .SingleOrDefaultAsync(
                vehicle => vehicle.Id == vehicleId,
                cancellationToken);
    }
}
