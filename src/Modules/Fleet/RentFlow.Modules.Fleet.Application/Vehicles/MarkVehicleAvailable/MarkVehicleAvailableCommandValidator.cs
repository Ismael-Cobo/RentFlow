using FluentValidation;

namespace RentFlow.Modules.Fleet.Application.Vehicles.MarkVehicleAvailable;

internal sealed class MarkVehicleAvailableCommandValidator
    : AbstractValidator<MarkVehicleAvailableCommand>
{
    public MarkVehicleAvailableCommandValidator()
    {
        RuleFor(command => command.VehicleId).NotEmpty();
    }
}
