using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.GetBooking;

internal sealed class GetBookingQueryHandler(IBookingRepository bookingRepository)
    : IQueryHandler<GetBookingQuery, GetBookingResponse>
{
    public async Task<Result<GetBookingResponse>> Handle(
        GetBookingQuery request,
        CancellationToken cancellationToken)
    {
        Booking? booking = await bookingRepository.GetBookingAsync(
            request.BookingId,
            cancellationToken);

        if (booking is null)
        {
            return Result.Failure<GetBookingResponse>(BookingErrors.NotFound(request.BookingId));
        }

        return Result.Success(GetBookingResponse.FromBooking(booking));
    }
}
