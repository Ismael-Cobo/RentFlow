using RentFlow.Common.Domain;

namespace RentFlow.Modules.Rentals.Domain.Booking.DomainEvents;

public sealed class BookingCreatedDomainEvent(Guid bookingId) : DomainEvent
{
    public Guid BookingId { get; init; } = bookingId;
}
