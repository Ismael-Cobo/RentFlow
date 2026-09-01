using RentFlow.Common.Application.Messaging;
using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Application.Vehicles.CreateVehicle;

public sealed record CreateVehicleCommand(
    string Brand,
    string Model,
    string LicensePlate,
    VehicleCategory Category,
    int DailyPrice)
    : ICommand<Guid>;
