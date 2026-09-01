using FluentValidation;

namespace RentFlow.Modules.Rentals.Application.Bookings.RemoveBookingExtra;

internal sealed class RemoveBookingExtraCommandValidator : AbstractValidator<RemoveBookingExtraCommand>
{
    public RemoveBookingExtraCommandValidator()
    {
        RuleFor(command => command.BookingId).NotEmpty();
        RuleFor(command => command.BookingExtraId).NotEmpty();
    }
}
