using RentFlow.Common.Domain;

namespace RentFlow.Modules.Fleet.Domain.Vehicle;

public static class VehicleErrors
{
    public static Error NotFound(Guid vehicleId) =>
        Error.NotFound(
            "Vehicle.NotFound",
            $"The vehicle with the identifier {vehicleId} was not found");

    public static Error LicensePlateAlreadyExists(string licensePlate) =>
        Error.Conflict(
            "Vehicle.LicensePlateAlreadyExists",
            $"A vehicle with the license plate {licensePlate} already exists");

    public static readonly Error NotAvailableForReservation = Error.Conflict(
        "Vehicle.NotAvailableForReservation",
        "Only available vehicles can be reserved");

    public static readonly Error NotReserved = Error.Conflict(
        "Vehicle.NotReserved",
        "Only reserved vehicles can be released");

    public static readonly Error NotAvailableToMarkUnavailable = Error.Conflict(
        "Vehicle.NotAvailableToMarkUnavailable",
        "Only available vehicles can be marked unavailable");

    public static readonly Error NotUnavailable = Error.Conflict(
        "Vehicle.NotUnavailable",
        "Only unavailable vehicles can be marked available");
}
