using RentFlow.Common.Application.Messaging;
using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Application.Vehicles.GetAvailableVehicles;

public sealed record GetAvailableVehiclesQuery(VehicleCategory? Category)
    : IQuery<IReadOnlyCollection<GetAvailableVehiclesResponse>>;
