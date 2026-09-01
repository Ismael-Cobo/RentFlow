namespace RentFlow.Modules.Rentals.Domain.Booking;

public interface IBookingRepository
{
    Task<Booking?> GetBookingAsync(
        Guid bookingId,
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Booking>> GetBookingsAsync(
        CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Booking>> GetBookingsByCustomerAsync(
        Guid customerId,
        CancellationToken cancellationToken);

    void Insert(Booking booking);
}
