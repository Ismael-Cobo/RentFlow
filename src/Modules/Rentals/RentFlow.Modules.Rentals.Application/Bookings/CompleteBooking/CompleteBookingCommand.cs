using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Rentals.Application.Bookings.CompleteBooking;

public sealed record CompleteBookingCommand(Guid BookingId) : ICommand;
