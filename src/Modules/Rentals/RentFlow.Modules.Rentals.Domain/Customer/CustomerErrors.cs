using RentFlow.Common.Domain;

namespace RentFlow.Modules.Rentals.Domain.Customer;

public static class CustomerErrors
{
    public static Error NotFound(Guid customerId) =>
        Error.NotFound(
            "Customer.NotFound",
            $"The customer with the identifier {customerId} was not found");
}
