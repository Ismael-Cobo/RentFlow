using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Rentals.Application.Bookings.GetBooking;

namespace RentFlow.Modules.Rentals.Presentation.Bookings;

internal sealed class GetBooking : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("bookings/{id}", async (Guid id, ISender sender) =>
            {
                Result<GetBookingResponse> result = await sender.Send(new GetBookingQuery(id));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Bookings);
    }
}
