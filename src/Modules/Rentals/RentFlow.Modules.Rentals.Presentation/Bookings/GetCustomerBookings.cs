using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RentFlow.Common.Domain;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Common.Presentation.Results;
using RentFlow.Modules.Rentals.Application.Bookings.GetCustomerBookings;

namespace RentFlow.Modules.Rentals.Presentation.Bookings;

internal sealed class GetCustomerBookings : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{customerId}/bookings", async (Guid customerId, ISender sender) =>
            {
                Result<IReadOnlyCollection<GetCustomerBookingsResponse>> result =
                    await sender.Send(new GetCustomerBookingsQuery(customerId));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .WithTags(Tags.Bookings);
    }
}
