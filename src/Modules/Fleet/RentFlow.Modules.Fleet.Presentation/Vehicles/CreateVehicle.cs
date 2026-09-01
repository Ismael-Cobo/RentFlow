using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Fleet.Application.Vehicles.CreateVehicle;
using RentFlow.Modules.Fleet.Domain.Vehicle;

namespace RentFlow.Modules.Fleet.Presentation.Vehicles;

internal sealed class CreateVehicle : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("vehicles", async (Request request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateVehicleCommand(
                    request.Brand,
                    request.Model,
                    request.LicensePlate,
                    request.Category,
                    request.DailyPrice));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Vehicles);
    }

    internal sealed class Request
    {
        public string Brand { get; init; } = string.Empty;

        public string Model { get; init; } = string.Empty;

        public string LicensePlate { get; init; } = string.Empty;

        public VehicleCategory Category { get; init; }

        public int DailyPrice { get; init; }
    }
}
