using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Application.Abstractions.Data;
using RentFlow.Modules.Rentals.Application.Abstractions.Pricing;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Application.Bookings.AddBookingExtra;

internal sealed class AddBookingExtraCommandHandler(
    IBookingRepository bookingRepository,
    IBookingExtraPriceProvider bookingExtraPriceProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AddBookingExtraCommand>
{
    public async Task<Result> Handle(
        AddBookingExtraCommand request,
        CancellationToken cancellationToken)
    {
        Booking? booking = await bookingRepository.GetBookingAsync(
            request.BookingId,
            cancellationToken);

        if (booking is null)
        {
            return Result.Failure(BookingErrors.NotFound(request.BookingId));
        }

        int? price = await bookingExtraPriceProvider.GetPriceAsync(
            request.Type,
            cancellationToken);

        if (price is null)
        {
            return Result.Failure(BookingErrors.ExtraPriceNotConfigured(request.Type));
        }

        Result addExtraResult = booking.AddExtra(
            Guid.CreateVersion7(),
            request.Type,
            price.Value);

        if (addExtraResult.IsFailure)
        {
            return addExtraResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return addExtraResult;
    }
}
