using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Rentals.Application.Bookings.CreateBooking;

public sealed record CreateBookingCommand(
    Guid CustomerId,
    Guid VehicleId,
    DateOnly StartPeriod,
    DateOnly EndPeriod) : ICommand<Guid>;
