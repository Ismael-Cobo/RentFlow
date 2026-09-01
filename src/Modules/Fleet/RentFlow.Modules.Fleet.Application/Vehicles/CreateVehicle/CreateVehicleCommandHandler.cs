using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Fleet.Application.Abstractions.Data;
using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Application.Vehicles.CreateVehicle;

internal sealed class CreateVehicleCommandHandler(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateVehicleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateVehicleCommand request,
        CancellationToken cancellationToken)
    {
        string licensePlate = request.LicensePlate.Trim().ToUpperInvariant();

        if (await vehicleRepository.ExistsByLicensePlateAsync(licensePlate, cancellationToken))
        {
            return Result.Failure<Guid>(VehicleErrors.LicensePlateAlreadyExists(licensePlate));
        }

        var vehicleId = Guid.CreateVersion7();
        var vehicle = Vehicle.Create(
            vehicleId,
            request.Brand,
            request.Model,
            licensePlate,
            request.Category,
            request.DailyPrice);

        vehicleRepository.Insert(vehicle);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return vehicleId;
    }
}
