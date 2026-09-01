using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Domain.Customer.DomainEvent;

namespace RentFlow.Modules.Rentals.Domain.Customer;

public sealed class Customer : Entity
{
    private Customer() { }

    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public static Customer Create(
        Guid userId,
        string email,
        string firstName,
        string lastName)
    {
        var customer = new Customer
        {
            Id = userId,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            CreatedAt = DateTime.UtcNow
        };

        customer.Raise(new CustomerCreatedDomainEvent(
            customer.Id,
            customer.Email,
            customer.FirstName,
            customer.LastName));

        return customer;
    }
}
