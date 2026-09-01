using FluentValidation;

namespace RentFlow.Modules.Fleet.Application.Vehicles.ReserveVehicle;

internal sealed class ReserveVehicleCommandValidator : AbstractValidator<ReserveVehicleCommand>
{
    public ReserveVehicleCommandValidator()
    {
        RuleFor(command => command.VehicleId).NotEmpty();
    }
}
