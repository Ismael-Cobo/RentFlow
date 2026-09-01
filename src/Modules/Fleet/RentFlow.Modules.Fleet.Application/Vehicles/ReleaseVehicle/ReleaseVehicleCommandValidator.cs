using FluentValidation;

namespace RentFlow.Modules.Fleet.Application.Vehicles.ReleaseVehicle;

internal sealed class ReleaseVehicleCommandValidator : AbstractValidator<ReleaseVehicleCommand>
{
    public ReleaseVehicleCommandValidator()
    {
        RuleFor(command => command.VehicleId).NotEmpty();
    }
}
