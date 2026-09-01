using System.Globalization;
using Microsoft.Extensions.Configuration;
using RentFlow.Modules.Rentals.Application.Abstractions.Pricing;
using RentFlow.Modules.Rentals.Domain.Booking;

namespace RentFlow.Modules.Rentals.Infrastructure.Pricing;

internal sealed class BookingExtraPriceProvider(IConfiguration configuration)
    : IBookingExtraPriceProvider
{
    private readonly IConfiguration _configuration = configuration;

    public Task<int?> GetPriceAsync(
        BookingExtraType type,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string? configuredPrice =
            _configuration[$"Rentals:BookingExtraPrices:{type}"];

        if (!int.TryParse(
                configuredPrice,
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out int price) ||
            price < 0)
        {
            return Task.FromResult<int?>(null);
        }

        return Task.FromResult<int?>(price);
    }
}
