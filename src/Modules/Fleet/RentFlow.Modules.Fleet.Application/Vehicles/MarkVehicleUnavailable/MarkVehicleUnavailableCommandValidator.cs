using FluentValidation;

namespace RentFlow.Modules.Fleet.Application.Vehicles.MarkVehicleUnavailable;

internal sealed class MarkVehicleUnavailableCommandValidator
    : AbstractValidator<MarkVehicleUnavailableCommand>
{
    public MarkVehicleUnavailableCommandValidator()
    {
        RuleFor(command => command.VehicleId).NotEmpty();
    }
}
