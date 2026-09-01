namespace RentFlow.Modules.Fleet.Domain.Vehicle;

public interface IVehicleRepository
{
    Task<Vehicle?> GetVehicleAsync(Guid vehicleId, CancellationToken cancellationToken);
    Task<bool> ExistsByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Vehicle>> GetVehiclesAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Vehicle>> GetAvailableVehiclesAsync(
        VehicleCategory? category,
        CancellationToken cancellationToken);
    void Insert(Vehicle vehicle);
}
