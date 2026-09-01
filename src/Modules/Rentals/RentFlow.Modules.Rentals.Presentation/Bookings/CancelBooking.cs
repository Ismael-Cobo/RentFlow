using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Rentals.Application.Bookings.CancelBooking;

namespace RentFlow.Modules.Rentals.Presentation.Bookings;

internal sealed class CancelBooking : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("bookings/{id}/cancel", async (Guid id, ISender sender) =>
            {
                Result result = await sender.Send(new CancelBookingCommand(id));

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .WithTags(Tags.Bookings);
    }
}
