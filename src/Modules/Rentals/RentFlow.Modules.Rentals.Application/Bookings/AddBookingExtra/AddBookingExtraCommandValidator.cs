using FluentValidation;

namespace RentFlow.Modules.Rentals.Application.Bookings.AddBookingExtra;

internal sealed class AddBookingExtraCommandValidator : AbstractValidator<AddBookingExtraCommand>
{
    public AddBookingExtraCommandValidator()
    {
        RuleFor(command => command.BookingId).NotEmpty();
        RuleFor(command => command.Type).IsInEnum();
    }
}
