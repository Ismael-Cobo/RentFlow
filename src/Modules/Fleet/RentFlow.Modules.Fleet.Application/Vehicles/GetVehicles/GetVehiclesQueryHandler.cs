using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Application.Vehicles.GetVehicles;

internal sealed class GetVehiclesQueryHandler(IVehicleRepository vehicleRepository)
    : IQueryHandler<GetVehiclesQuery, IReadOnlyCollection<GetVehiclesResponse>>
{
    public async Task<Result<IReadOnlyCollection<GetVehiclesResponse>>> Handle(
        GetVehiclesQuery request,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Vehicle> vehicles = await vehicleRepository.GetVehiclesAsync(
            cancellationToken);

        IReadOnlyCollection<GetVehiclesResponse> response = vehicles
            .Select(GetVehiclesResponse.FromVehicle)
            .ToArray();

        return Result.Success(response);
    }
}
