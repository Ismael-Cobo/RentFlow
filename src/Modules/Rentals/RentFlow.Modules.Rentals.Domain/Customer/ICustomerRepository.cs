namespace RentFlow.Modules.Rentals.Domain.Customer;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerAsync(
        Guid customerId,
        CancellationToken cancellationToken);

    void Insert(Customer customer);
}
