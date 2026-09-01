using FluentValidation;

namespace RentFlow.Modules.Rentals.Application.Bookings.CreateBooking;

internal sealed class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(command => command.CustomerId).NotEmpty();
        RuleFor(command => command.VehicleId).NotEmpty();
        RuleFor(command => command.StartPeriod).NotEmpty();
        RuleFor(command => command.EndPeriod).NotEmpty();
    }
}
