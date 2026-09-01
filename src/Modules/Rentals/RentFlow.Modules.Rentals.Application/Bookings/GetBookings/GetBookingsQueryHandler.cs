using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.GetBookings;

internal sealed class GetBookingsQueryHandler(IBookingRepository bookingRepository)
    : IQueryHandler<GetBookingsQuery, IReadOnlyCollection<GetBookingsResponse>>
{
    public async Task<Result<IReadOnlyCollection<GetBookingsResponse>>> Handle(
        GetBookingsQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Booking> bookings = await bookingRepository.GetBookingsAsync(
            cancellationToken);

        IReadOnlyCollection<GetBookingsResponse> response = bookings
            .Select(GetBookingsResponse.FromBooking)
            .ToArray();

        return Result.Success(response);
    }
}
