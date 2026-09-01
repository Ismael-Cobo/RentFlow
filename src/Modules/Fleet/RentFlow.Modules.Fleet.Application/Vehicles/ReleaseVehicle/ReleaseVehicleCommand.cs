using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Fleet.Application.Vehicles.ReleaseVehicle;

public sealed record ReleaseVehicleCommand(Guid VehicleId) : ICommand;
