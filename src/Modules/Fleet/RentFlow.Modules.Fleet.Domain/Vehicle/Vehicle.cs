using RentFlow.Common.Domain;
using RentFlow.Modules.Fleet.Domain.Vehicle.DomainEvents;

namespace RentFlow.Modules.Fleet.Domain.Vehicle;

public sealed class Vehicle : Entity
{
    private Vehicle() { }

    public Guid Id { get; private set; }
    public string Brand { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;
    public string LicensePlate { get; private set; } = string.Empty;
    public VehicleCategory Category { get; private set; }
    public int DailyPrice { get; private set; }
    public VehicleStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static Vehicle Create(
        Guid id,
        string brand,
        string model,
        string licensePlate,
        VehicleCategory category,
        int dailyPrice)
    {
        var vehicle = new Vehicle
        {
            Id = id,
            Brand = brand,
            Model = model,
            LicensePlate = licensePlate.Trim().ToUpperInvariant(),
            Category = category,
            DailyPrice = dailyPrice,
            Status = VehicleStatus.Available,
            CreatedAt = DateTime.UtcNow
        };

        vehicle.Raise(new VehicleCreatedDomainEvent(vehicle.Id));

        return vehicle;
    }

    public Result Reserve()
    {
        if (Status != VehicleStatus.Available)
        {
            return Result.Failure(VehicleErrors.NotAvailableForReservation);
        }

        Status = VehicleStatus.Reserved;
        Raise(new VehicleReservedDomainEvent(Id));

        return Result.Success();
    }

    public Result Release()
    {
        if (Status != VehicleStatus.Reserved)
        {
            return Result.Failure(VehicleErrors.NotReserved);
        }

        Status = VehicleStatus.Available;
        Raise(new VehicleReleasedDomainEvent(Id));

        return Result.Success();
    }

    public Result MarkUnavailable()
    {
        if (Status != VehicleStatus.Available)
        {
            return Result.Failure(VehicleErrors.NotAvailableToMarkUnavailable);
        }

        Status = VehicleStatus.Unavailable;
        Raise(new VehicleMarkedUnavailableDomainEvent(Id));

        return Result.Success();
    }

    public Result MarkAvailable()
    {
        if (Status != VehicleStatus.Unavailable)
        {
            return Result.Failure(VehicleErrors.NotUnavailable);
        }

        Status = VehicleStatus.Available;
        Raise(new VehicleMarkedAvailableDomainEvent(Id));

        return Result.Success();
    }
}
