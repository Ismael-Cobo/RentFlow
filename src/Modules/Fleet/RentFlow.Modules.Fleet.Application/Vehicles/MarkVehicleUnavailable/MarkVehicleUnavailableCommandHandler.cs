using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Fleet.Application.Abstractions.Data;
using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Application.Vehicles.MarkVehicleUnavailable;

internal sealed class MarkVehicleUnavailableCommandHandler(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<MarkVehicleUnavailableCommand>
{
    public async Task<Result> Handle(
        MarkVehicleUnavailableCommand request,
        CancellationToken cancellationToken)
    {
        Vehicle? vehicle = await vehicleRepository.GetVehicleAsync(
            request.VehicleId,
            cancellationToken);

        if (vehicle is null)
        {
            return Result.Failure(VehicleErrors.NotFound(request.VehicleId));
        }

        Result transitionResult = vehicle.MarkUnavailable();

        if (transitionResult.IsFailure)
        {
            return transitionResult;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return transitionResult;
    }
}
