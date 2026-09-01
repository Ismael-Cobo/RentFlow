using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Rentals.Application.Bookings.GetBooking;

public sealed record GetBookingQuery(Guid BookingId) : IQuery<GetBookingResponse>;
