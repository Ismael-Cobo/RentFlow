using Microsoft.EntityFrameworkCore;
using RentFlow.Modules.Rentals.Domain.Customer;
using RentFlow.Modules.Rentals.Infrastructure.Database;

namespace RentFlow.Modules.Rentals.Infrastructure.Customers;

internal sealed class CustomerRepository(RentalsDbContext dbContext) : ICustomerRepository
{
    private readonly RentalsDbContext _dbContext = dbContext;

    public async Task<Customer?> GetCustomerAsync(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Customers
            .AsNoTracking()
            .SingleOrDefaultAsync(
                customer => customer.Id == customerId,
                cancellationToken);
    }

    public void Insert(Customer customer)
    {
        _dbContext.Customers.Add(customer);
    }
}
