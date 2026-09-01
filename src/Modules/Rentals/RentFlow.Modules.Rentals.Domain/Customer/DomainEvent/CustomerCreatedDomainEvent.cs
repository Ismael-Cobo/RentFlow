namespace RentFlow.Modules.Rentals.Domain.Customer.DomainEvent;

public sealed class CustomerCreatedDomainEvent(
    Guid customerId,
    string email,
    string firstName,
    string lastName) : Common.Domain.DomainEvent
{
    public Guid CustomerId { get; init; } = customerId;
    public string Email { get; init; } = email;
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
}
