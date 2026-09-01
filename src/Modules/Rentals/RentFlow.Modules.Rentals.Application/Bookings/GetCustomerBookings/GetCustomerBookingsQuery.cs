using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Rentals.Application.Bookings.GetCustomerBookings;

public sealed record GetCustomerBookingsQuery(Guid CustomerId)
    : IQuery<IReadOnlyCollection<GetCustomerBookingsResponse>>;
