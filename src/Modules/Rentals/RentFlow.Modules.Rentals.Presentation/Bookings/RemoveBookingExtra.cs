using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Rentals.Application.Bookings.RemoveBookingExtra;

namespace RentFlow.Modules.Rentals.Presentation.Bookings;

internal sealed class RemoveBookingExtra : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "bookings/{id}/extras/{bookingExtraId}",
                async (Guid id, Guid bookingExtraId, ISender sender) =>
                {
                    Result result = await sender.Send(
                        new RemoveBookingExtraCommand(id, bookingExtraId));

                    return result.Match(Results.NoContent, ApiResults.Problem);
                })
            .WithTags(Tags.Bookings);
    }
}
