using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Application.Abstractions.Data;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.CancelBooking;

internal sealed class CancelBookingCommandHandler(
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CancelBookingCommand>
{
    public async Task<Result> Handle(
        CancelBookingCommand request,
        CancellationToken cancellationToken)
    {
        Booking? booking = await bookingRepository.GetBookingAsync(
            request.BookingId,
            cancellationToken);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound(request.BookingId));
        }

        Result cancellationResult = booking.Cancel();

        if (cancellationResult.IsFailure)
        {
            return cancellationResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return cancellationResult;
    }
}
