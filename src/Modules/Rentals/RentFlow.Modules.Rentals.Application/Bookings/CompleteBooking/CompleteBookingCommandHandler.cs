using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Application.Abstractions.Data;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.CompleteBooking;

internal sealed class CompleteBookingCommandHandler(
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CompleteBookingCommand>
{
    public async Task<Result> Handle(
        CompleteBookingCommand request,
        CancellationToken cancellationToken)
    {
        Booking? booking = await bookingRepository.GetBookingAsync(
            request.BookingId,
            cancellationToken);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound(request.BookingId));
        }

        Result completionResult = booking.Complete();

        if (completionResult.IsFailure)
        {
            return completionResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return completionResult;
    }
}
