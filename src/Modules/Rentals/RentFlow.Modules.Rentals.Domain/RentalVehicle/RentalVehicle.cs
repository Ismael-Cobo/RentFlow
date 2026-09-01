using RentFlow.Common.Domain;

namespace RentFlow.Modules.Rentals.Domain.RentalVehicle;

public sealed class RentalVehicle : Entity
{
    private RentalVehicle() { }

    public Guid Id { get; private set; }
    public int DailyPrice { get; private set; }
    public bool IsAvailable { get; private set; }

    public static Result<RentalVehicle> Create(
        Guid id,
        int dailyPrice,
        bool isAvailable)
    {
        if (dailyPrice <= 0)
        {
            return Result.Failure<RentalVehicle>(RentalVehicleErrors.InvalidDailyPrice);
        }

        var rentalVehicle = new RentalVehicle
        {
            Id = id,
            DailyPrice = dailyPrice,
            IsAvailable = isAvailable
        };

        return Result.Success(rentalVehicle);
    }

    public Result Update(int dailyPrice, bool isAvailable)
    {
        if (dailyPrice <= 0)
        {
            return Result.Failure(RentalVehicleErrors.InvalidDailyPrice);
        }

        DailyPrice = dailyPrice;
        IsAvailable = isAvailable;

        return Result.Success();
    }
}
