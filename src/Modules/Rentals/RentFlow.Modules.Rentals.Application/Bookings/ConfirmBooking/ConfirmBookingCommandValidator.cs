using FluentValidation;

namespace RentFlow.Modules.Rentals.Application.Bookings.ConfirmBooking;

internal sealed class ConfirmBookingCommandValidator : AbstractValidator<ConfirmBookingCommand>
{
    public ConfirmBookingCommandValidator()
    {
        RuleFor(command => command.BookingId).NotEmpty();
    }
}
