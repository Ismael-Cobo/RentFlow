using RentFlow.Common.Domain;

namespace RentFlow.Modules.Fleet.Domain.Vehicle.DomainEvents;

public sealed class VehicleReleasedDomainEvent(Guid vehicleId) : DomainEvent
{
    public Guid VehicleId { get; init; } = vehicleId;
}
