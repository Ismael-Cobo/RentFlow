using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Application.Vehicles.GetVehicle;

internal sealed class GetVehicleQueryHandler(IVehicleRepository vehicleRepository)
    : IQueryHandler<GetVehicleQuery, GetVehicleResponse>
{
    public async Task<Result<GetVehicleResponse>> Handle(
        GetVehicleQuery request,
        CancellationToken cancellationToken)
    {
        Vehicle? vehicle = await vehicleRepository.GetVehicleAsync(
            request.VehicleId,
            cancellationToken);

        if (vehicle is null)
        {
            return Result.Failure<GetVehicleResponse>(VehicleErrors.NotFound(request.VehicleId));
        }

        return Result.Success(GetVehicleResponse.FromVehicle(vehicle));
    }
}
