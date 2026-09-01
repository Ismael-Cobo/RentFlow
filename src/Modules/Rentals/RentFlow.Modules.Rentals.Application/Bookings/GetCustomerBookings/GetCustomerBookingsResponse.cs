using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.GetCustomerBookings;

public sealed record GetCustomerBookingsResponse(
    Guid Id,
    Guid CustomerId,
    Guid VehicleId,
    DateOnly StartPeriod,
    DateOnly EndPeriod,
    BookingStatus Status,
    int BasePrice,
    int TotalPrice,
    DateTime CreatedAt)
{
    internal static GetCustomerBookingsResponse FromBooking(Booking booking) =>
        new(
            booking.Id,
            booking.CustomerId,
            booking.VehicleId,
            booking.StartPeriod,
            booking.EndPeriod,
            booking.Status,
            booking.BasePrice,
            booking.TotalPrice,
            booking.CreatedAt);
}
