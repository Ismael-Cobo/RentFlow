using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.GetBooking;

public sealed record GetBookingExtraResponse(
    Guid Id,
    BookingExtraType Type,
    int Price)
{
    internal static GetBookingExtraResponse FromBookingExtra(BookingExtra extra) =>
        new(
            extra.Id,
            extra.Type,
            extra.Price);
}
