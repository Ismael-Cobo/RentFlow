using Microsoft.AspNetCore.Routing;

namespace RentFlow.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
