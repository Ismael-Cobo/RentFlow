using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Fleet.Application.Vehicles.MarkVehicleAvailable;

public sealed record MarkVehicleAvailableCommand(Guid VehicleId) : ICommand;
