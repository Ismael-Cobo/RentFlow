using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Fleet.Application.Vehicles.GetVehicles;

namespace RentFlow.Modules.Fleet.Presentation.Vehicles;

internal sealed class GetVehicles : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("vehicles", async (ISender sender) =>
            {
                Result<IReadOnlyCollection<GetVehiclesResponse>> result =
                    await sender.Send(new GetVehiclesQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Vehicles);
    }
}
