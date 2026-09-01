using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Rentals.Application.Bookings.RemoveBookingExtra;

public sealed record RemoveBookingExtraCommand(
    Guid BookingId,
    Guid BookingExtraId) : ICommand;
