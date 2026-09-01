using RentFlow.Common.Domain;

namespace RentFlow.Modules.Rentals.Domain.RentalVehicle;

public static class RentalVehicleErrors
{
    public static Error NotFound(Guid vehicleId) =>
        Error.NotFound(
            "RentalVehicle.NotFound",
            $"The rental vehicle with the identifier {vehicleId} was not found");

    public static Error Unavailable(Guid vehicleId) =>
        Error.Conflict(
            "RentalVehicle.Unavailable",
            $"The rental vehicle with the identifier {vehicleId} is unavailable");

    public static readonly Error InvalidDailyPrice = new(
        "RentalVehicle.InvalidDailyPrice",
        "The rental vehicle daily price must be greater than zero",
        ErrorType.Validation);
}
