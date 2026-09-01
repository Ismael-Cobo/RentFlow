using RentFlow.Common.Domain;

namespace RentFlow.Modules.Rentals.Domain.Booking.DomainEvents;

public sealed class BookingExtraRemovedDomainEvent(
    Guid bookingId,
    Guid bookingExtraId) : DomainEvent
{
    public Guid BookingId { get; init; } = bookingId;
    public Guid BookingExtraId { get; init; } = bookingExtraId;
}
