namespace RentFlow.Modules.Rentals.Domain.RentalVehicle;

public interface IRentalVehicleRepository
{
    Task<RentalVehicle?> GetRentalVehicleAsync(
        Guid vehicleId,
        CancellationToken cancellationToken);
}
