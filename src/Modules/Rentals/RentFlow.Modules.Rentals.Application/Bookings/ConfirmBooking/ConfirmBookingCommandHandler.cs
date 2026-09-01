using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Application.Abstractions.Data;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.ConfirmBooking;

internal sealed class ConfirmBookingCommandHandler(
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ConfirmBookingCommand>
{
    public async Task<Result> Handle(
        ConfirmBookingCommand request,
        CancellationToken cancellationToken)
    {
        Booking? booking = await bookingRepository.GetBookingAsync(
            request.BookingId,
            cancellationToken);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound(request.BookingId));
        }

        Result confirmationResult = booking.Confirm();

        if (confirmationResult.IsFailure)
        {
            return confirmationResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return confirmationResult;
    }
}
