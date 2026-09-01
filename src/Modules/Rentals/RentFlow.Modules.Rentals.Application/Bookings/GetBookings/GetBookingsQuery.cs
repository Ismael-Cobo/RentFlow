using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Rentals.Application.Bookings.GetBookings;

public sealed record GetBookingsQuery : IQuery<IReadOnlyCollection<GetBookingsResponse>>;
