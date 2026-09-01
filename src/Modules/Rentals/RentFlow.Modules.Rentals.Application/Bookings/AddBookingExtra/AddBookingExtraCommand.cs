using RentFlow.Common.Application.Messaging;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.AddBookingExtra;

public sealed record AddBookingExtraCommand(
    Guid BookingId,
    BookingExtraType Type) : ICommand;
