using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Rentals.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(Guid CustomerId, string Email, string FirstName, string LastName) 
    : ICommand;
