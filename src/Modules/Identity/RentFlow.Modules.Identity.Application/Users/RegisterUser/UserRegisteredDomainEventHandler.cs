using MediatR;
using RentFlow.Common.Application.EventBus;
using RentFlow.Common.Application.Exceptions;
using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Identity.Application.Users.GetUser;
using RentFlow.Modules.Identity.Domain.User.DomainEvent;
using RentFlow.Modules.Identity.IntegrationEvents;

namespace RentFlow.Modules.Identity.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus eventBus) : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        Result<UserResponse> result = await sender.Send(new GetUserQuery(notification.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new RentFlowException(nameof(GetUserQuery),  result.Error);
        }

        await eventBus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                result.Value.Id,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
