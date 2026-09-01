using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Rentals.Application.Bookings.CreateBooking;

namespace RentFlow.Modules.Rentals.Presentation.Bookings;

internal sealed class CreateBooking : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("bookings", async (Request request, ISender sender) =>
            {
                Result<Guid> result = await sender.Send(new CreateBookingCommand(
                    request.CustomerId,
                    request.VehicleId,
                    request.StartPeriod,
                    request.EndPeriod));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Bookings);
    }

    internal sealed class Request
    {
        public Guid CustomerId { get; init; }

        public Guid VehicleId { get; init; }

        public DateOnly StartPeriod { get; init; }

        public DateOnly EndPeriod { get; init; }
    }
}
