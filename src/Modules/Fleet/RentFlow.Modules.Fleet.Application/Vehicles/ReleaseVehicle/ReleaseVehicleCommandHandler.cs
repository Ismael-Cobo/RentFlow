using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Fleet.Application.Abstractions.Data;
using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Application.Vehicles.ReleaseVehicle;

internal sealed class ReleaseVehicleCommandHandler(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ReleaseVehicleCommand>
{
    public async Task<Result> Handle(
        ReleaseVehicleCommand request,
        CancellationToken cancellationToken)
    {
        Vehicle? vehicle = await vehicleRepository.GetVehicleAsync(
            request.VehicleId,
            cancellationToken);

        if (vehicle is null)
        {
            return Result.Failure(VehicleErrors.NotFound(request.VehicleId));
        }

        Result transitionResult = vehicle.Release();

        if (transitionResult.IsFailure)
        {
            return transitionResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return transitionResult;
    }
}
