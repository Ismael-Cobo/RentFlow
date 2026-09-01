using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Rentals.Application.Bookings.GetBookings;

namespace RentFlow.Modules.Rentals.Presentation.Bookings;

internal sealed class GetBookings : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("bookings", async (ISender sender) =>
            {
                Result<IReadOnlyCollection<GetBookingsResponse>> result =
                    await sender.Send(new GetBookingsQuery());

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Bookings);
    }
}
