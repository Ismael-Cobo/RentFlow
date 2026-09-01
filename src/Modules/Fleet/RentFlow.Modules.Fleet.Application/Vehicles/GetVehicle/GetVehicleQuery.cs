using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Fleet.Application.Vehicles.GetVehicle;

public sealed record GetVehicleQuery(Guid VehicleId) : IQuery<GetVehicleResponse>;
