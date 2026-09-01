using Microsoft.EntityFrameworkCore;
using RentFlow.Modules.Rentals.Domain.Booking;
using RentFlow.Modules.Rentals.Infrastructure.Database;

namespace RentFlow.Modules.Rentals.Infrastructure.Bookings;

internal sealed class BookingRepository(RentalsDbContext dbContext) : IBookingRepository
{
    private readonly RentalsDbContext _dbContext = dbContext;

    public async Task<Booking?> GetBookingAsync(
        Guid bookingId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Bookings
            .Include(booking => booking.Extras)
            .SingleOrDefaultAsync(
                booking => booking.Id == bookingId,
                cancellationToken);
    }

    public async Task<IReadOnlyCollection<Booking>> GetBookingsAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Bookings
            .AsNoTracking()
            .OrderByDescending(booking => booking.CreatedAt)
            .ThenBy(booking => booking.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<Booking>> GetBookingsByCustomerAsync(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Bookings
            .AsNoTracking()
            .Where(booking => booking.CustomerId == customerId)
            .OrderByDescending(booking => booking.CreatedAt)
            .ThenBy(booking => booking.Id)
            .ToListAsync(cancellationToken);
    }

    public void Insert(Booking booking)
    {
        _dbContext.Bookings.Add(booking);
    }
}
