using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Application.Abstractions.Data;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.RemoveBookingExtra;

internal sealed class RemoveBookingExtraCommandHandler(
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveBookingExtraCommand>
{
    public async Task<Result> Handle(
        RemoveBookingExtraCommand request,
        CancellationToken cancellationToken)
    {
        Booking? booking = await bookingRepository.GetBookingAsync(
            request.BookingId,
            cancellationToken);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound(request.BookingId));
        }

        Result removeExtraResult = booking.RemoveExtra(request.BookingExtraId);

        if (removeExtraResult.IsFailure)
        {
            return removeExtraResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return removeExtraResult;
    }
}
