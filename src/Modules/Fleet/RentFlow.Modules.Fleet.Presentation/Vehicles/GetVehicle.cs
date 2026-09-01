using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Fleet.Application.Vehicles.GetVehicle;

namespace RentFlow.Modules.Fleet.Presentation.Vehicles;

internal sealed class GetVehicle : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("vehicles/{id}", async (Guid id, ISender sender) =>
            {
                Result<GetVehicleResponse> result = await sender.Send(new GetVehicleQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Vehicles);
    }
}
