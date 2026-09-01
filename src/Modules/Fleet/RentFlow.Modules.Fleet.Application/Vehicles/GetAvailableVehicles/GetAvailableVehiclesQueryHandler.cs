using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Application.Vehicles.GetAvailableVehicles;

internal sealed class GetAvailableVehiclesQueryHandler(IVehicleRepository vehicleRepository)
    : IQueryHandler<GetAvailableVehiclesQuery, IReadOnlyCollection<GetAvailableVehiclesResponse>>
{
    public async Task<Result<IReadOnlyCollection<GetAvailableVehiclesResponse>>> Handle(
        GetAvailableVehiclesQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Vehicle> vehicles = await vehicleRepository.GetAvailableVehiclesAsync(
            request.Category,
            cancellationToken);

        IReadOnlyCollection<GetAvailableVehiclesResponse> response = vehicles
            .Select(GetAvailableVehiclesResponse.FromVehicle)
            .ToArray();

        return Result.Success(response);
    }
}
