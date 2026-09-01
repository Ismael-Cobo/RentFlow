using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Fleet.Application.Vehicles.GetVehicles;

public sealed record GetVehiclesQuery : IQuery<IReadOnlyCollection<GetVehiclesResponse>>;
