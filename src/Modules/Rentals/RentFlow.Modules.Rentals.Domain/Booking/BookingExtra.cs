namespace RentFlow.Modules.Rentals.Domain.Booking;

public sealed class BookingExtra
{
    private BookingExtra() { }

    public Guid Id { get; private set; }
    public BookingExtraType Type { get; private set; }
    public int Price { get; private set; }

    internal static BookingExtra Create(
        Guid id,
        BookingExtraType type,
        int price) =>
        new()
        {
            Id = id,
            Type = type,
            Price = price
        };
}
