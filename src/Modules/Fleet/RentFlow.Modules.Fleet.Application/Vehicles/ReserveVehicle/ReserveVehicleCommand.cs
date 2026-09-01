using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Fleet.Application.Vehicles.ReserveVehicle;

public sealed record ReserveVehicleCommand(Guid VehicleId) : ICommand;
