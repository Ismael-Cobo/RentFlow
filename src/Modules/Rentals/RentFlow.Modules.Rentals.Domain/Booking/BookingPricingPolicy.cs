using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Domain.RentalVehicle;

namespace RentFlow.Modules.Rentals.Domain.Booking;

public static class BookingPricingPolicy
{
    public static Result<int> CalculateBasePrice(
        int dailyPrice,
        DateOnly startPeriod,
        DateOnly endPeriod)
    {
        if (startPeriod >= endPeriod)
        {
            return Result.Failure<int>(BookingErrors.InvalidPeriod);
        }

        if (dailyPrice <= 0)
        {
            return Result.Failure<int>(RentalVehicleErrors.InvalidDailyPrice);
        }

        int rentalDays = endPeriod.DayNumber - startPeriod.DayNumber;

        try
        {
            return checked(dailyPrice * rentalDays);
        }
        catch (OverflowException)
        {
            return Result.Failure<int>(BookingErrors.BasePriceOverflow);
        }
    }
}
