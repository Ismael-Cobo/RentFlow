using FluentValidation;

namespace RentFlow.Modules.Rentals.Application.Bookings.CancelBooking;

internal sealed class CancelBookingCommandValidator : AbstractValidator<CancelBookingCommand>
{
    public CancelBookingCommandValidator()
    {
        RuleFor(command => command.BookingId).NotEmpty();
    }
}
