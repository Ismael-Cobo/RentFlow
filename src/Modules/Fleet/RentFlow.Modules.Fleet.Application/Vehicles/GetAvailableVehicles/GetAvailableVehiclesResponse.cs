using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Application.Vehicles.GetAvailableVehicles;

public sealed record GetAvailableVehiclesResponse(
    Guid Id,
    string Brand,
    string Model,
    string LicensePlate,
    VehicleCategory Category,
    int DailyPrice,
    VehicleStatus Status,
    DateTime CreatedAt)
{
    internal static GetAvailableVehiclesResponse FromVehicle(Vehicle vehicle) =>
        new(
            vehicle.Id,
            vehicle.Brand,
            vehicle.Model,
            vehicle.LicensePlate,
            vehicle.Category,
            vehicle.DailyPrice,
            vehicle.Status,
            vehicle.CreatedAt);
}
