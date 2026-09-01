using RentFlow.Common.Domain;

namespace RentFlow.Modules.Rentals.Domain.Booking.DomainEvents;

public sealed class BookingCancelledDomainEvent(Guid bookingId) : DomainEvent
{
    public Guid BookingId { get; init; } = bookingId;
}
