using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Rentals.Application.Bookings.CancelBooking;

public sealed record CancelBookingCommand(Guid BookingId) : ICommand;
