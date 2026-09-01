using FluentValidation;

namespace RentFlow.Modules.Fleet.Application.Vehicles.CreateVehicle;

internal sealed class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator()
    {
        RuleFor(command => command.Brand)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.Model)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(command => command.LicensePlate)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(command => command.Category).IsInEnum();
        RuleFor(command => command.DailyPrice).GreaterThan(0);
    }
}
