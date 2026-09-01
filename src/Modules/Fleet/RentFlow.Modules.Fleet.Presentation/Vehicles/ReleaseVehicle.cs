using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Fleet.Application.Vehicles.ReleaseVehicle;

namespace RentFlow.Modules.Fleet.Presentation.Vehicles;

internal sealed class ReleaseVehicle : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("vehicles/{id}/release", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(new ReleaseVehicleCommand(id));

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .WithTags(Tags.Vehicles);
    }
}
