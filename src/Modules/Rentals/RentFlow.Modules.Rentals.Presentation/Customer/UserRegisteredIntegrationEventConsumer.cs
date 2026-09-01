using MassTransit;
using MediatR;
using RentFlow.Common.Application.Exceptions;
using RentFlow.Common.Domain;
using RentFlow.Modules.Identity.IntegrationEvents;
using RentFlow.Modules.Rentals.Application.Customers.CreateCustomer;

namespace RentFlow.Modules.Rentals.Presentation.Customer;

public sealed class UserRegisteredIntegrationEventConsumer(ISender sender)
    : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new CreateCustomerCommand(
                context.Message.UserId,
                context.Message.Email,
                context.Message.FirstName,
                context.Message.LastName));

        if (result.IsFailure)
        {
            throw new RentFlowException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
