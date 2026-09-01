using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Fleet.Application.Vehicles.GetAvailableVehicles;
using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Presentation.Vehicles;

internal sealed class GetAvailableVehicles : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("vehicles/available", async (VehicleCategory? category, ISender sender) =>
            {
                Result<IReadOnlyCollection<GetAvailableVehiclesResponse>> result =
                    await sender.Send(new GetAvailableVehiclesQuery(category));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Vehicles);
    }
}
