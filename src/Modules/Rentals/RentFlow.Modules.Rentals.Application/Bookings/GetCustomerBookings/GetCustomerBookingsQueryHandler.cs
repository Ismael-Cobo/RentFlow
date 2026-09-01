using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.GetCustomerBookings;

internal sealed class GetCustomerBookingsQueryHandler(IBookingRepository bookingRepository)
    : IQueryHandler<GetCustomerBookingsQuery, IReadOnlyCollection<GetCustomerBookingsResponse>>
{
    public async Task<Result<IReadOnlyCollection<GetCustomerBookingsResponse>>> Handle(
        GetCustomerBookingsQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Booking> bookings = await bookingRepository.GetBookingsByCustomerAsync(
            request.CustomerId,
            cancellationToken);

        IReadOnlyCollection<GetCustomerBookingsResponse> response = bookings
            .Select(GetCustomerBookingsResponse.FromBooking)
            .ToArray();

        return Result.Success(response);
    }
}
