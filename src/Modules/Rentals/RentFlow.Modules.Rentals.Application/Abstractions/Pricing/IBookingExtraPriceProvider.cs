using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Abstractions.Pricing;

public interface IBookingExtraPriceProvider
{
    Task<int?> GetPriceAsync(
        BookingExtraType type,
        CancellationToken cancellationToken);
}
