using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Rentals.Application.Bookings.AddBookingExtra;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Presentation.Bookings;

internal sealed class AddBookingExtra : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("bookings/{id}/extras", async (Guid id, Request request, ISender sender) =>
            {
                Result result = await sender.Send(new AddBookingExtraCommand(id, request.Type));

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .WithTags(Tags.Bookings);
    }

    internal sealed class Request
    {
        public BookingExtraType Type { get; init; }
    }
}
