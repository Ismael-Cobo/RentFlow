using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Domain.Booking.DomainEvents;

namespace RentFlow.Modules.Rentals.Domain.Booking;

public sealed class Booking : Entity
{
    private readonly List<BookingExtra> _extras = [];

    private Booking() { }

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid VehicleId { get; private set; }
    public DateOnly StartPeriod { get; private set; }
    public DateOnly EndPeriod { get; private set; }
    public BookingStatus Status { get; private set; }
    public int BasePrice { get; private set; }
    public int TotalPrice { get; private set; }
    public IReadOnlyCollection<BookingExtra> Extras => _extras.AsReadOnly();
    public DateTime CreatedAt { get; private set; }

    public static Result<Booking> Create(
        Guid id,
        Guid customerId,
        Guid vehicleId,
        DateOnly startPeriod,
        DateOnly endPeriod,
        int basePrice)
    {
        if (startPeriod >= endPeriod)
        {
            return Result.Failure<Booking>(BookingErrors.InvalidPeriod);
        }

        if (basePrice < 0)
        {
            return Result.Failure<Booking>(BookingErrors.NegativeBasePrice);
        }

        var booking = new Booking
        {
            Id = id,
            CustomerId = customerId,
            VehicleId = vehicleId,
            StartPeriod = startPeriod,
            EndPeriod = endPeriod,
            Status = BookingStatus.Pending,
            BasePrice = basePrice,
            TotalPrice = basePrice,
            CreatedAt = DateTime.UtcNow
        };

        booking.Raise(new BookingCreatedDomainEvent(booking.Id));

        return Result.Success(booking);
    }

    public Result Confirm()
    {
        if (Status != BookingStatus.Pending)
        {
            return Result.Failure(BookingErrors.NotPendingForConfirmation);
        }

        Status = BookingStatus.Confirmed;
        Raise(new BookingConfirmedDomainEvent(Id));

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status is not BookingStatus.Pending and not BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotCancellable);
        }

        Status = BookingStatus.Cancelled;
        Raise(new BookingCancelledDomainEvent(Id));

        return Result.Success();
    }

    public Result Complete()
    {
        if (Status != BookingStatus.Confirmed)
        {
            return Result.Failure(BookingErrors.NotConfirmedForCompletion);
        }

        Status = BookingStatus.Completed;
        Raise(new BookingCompletedDomainEvent(Id));

        return Result.Success();
    }

    public Result AddExtra(
        Guid bookingExtraId,
        BookingExtraType type,
        int price)
    {
        if (Status != BookingStatus.Pending)
        {
            return Result.Failure(BookingErrors.ExtraCanOnlyBeAddedWhilePending);
        }

        if (price < 0)
        {
            return Result.Failure(BookingErrors.NegativeExtraPrice);
        }

        if (_extras.Exists(extra => extra.Type == type))
        {
            return Result.Failure(BookingErrors.DuplicateExtraType(type));
        }

        var extra = BookingExtra.Create(bookingExtraId, type, price);

        _extras.Add(extra);
        RecalculateTotalPrice();
        Raise(new BookingExtraAddedDomainEvent(Id, extra.Id));

        return Result.Success();
    }

    public Result RemoveExtra(Guid bookingExtraId)
    {
        if (Status != BookingStatus.Pending)
        {
            return Result.Failure(BookingErrors.ExtraCanOnlyBeRemovedWhilePending);
        }

        BookingExtra? extra = _extras.Find(item => item.Id == bookingExtraId);

        if (extra is null)
        {
            return Result.Failure(BookingErrors.ExtraNotFound(bookingExtraId));
        }

        _extras.Remove(extra);
        RecalculateTotalPrice();
        Raise(new BookingExtraRemovedDomainEvent(Id, extra.Id));

        return Result.Success();
    }

    private void RecalculateTotalPrice()
    {
        TotalPrice = BasePrice + _extras.Sum(extra => extra.Price);
    }
}
