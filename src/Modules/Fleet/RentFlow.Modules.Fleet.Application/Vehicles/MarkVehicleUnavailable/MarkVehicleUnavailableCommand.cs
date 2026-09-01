using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Fleet.Application.Vehicles.MarkVehicleUnavailable;

public sealed record MarkVehicleUnavailableCommand(Guid VehicleId) : ICommand;
