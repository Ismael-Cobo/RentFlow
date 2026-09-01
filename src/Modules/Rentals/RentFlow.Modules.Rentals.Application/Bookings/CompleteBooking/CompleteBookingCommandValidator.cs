using FluentValidation;

namespace RentFlow.Modules.Rentals.Application.Bookings.CompleteBooking;

internal sealed class CompleteBookingCommandValidator : AbstractValidator<CompleteBookingCommand>
{
    public CompleteBookingCommandValidator()
    {
        RuleFor(command => command.BookingId).NotEmpty();
    }
}
