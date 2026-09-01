using RentFlow.Common.Domain;

namespace RentFlow.Modules.Rentals.Domain.Booking;

public static class BookingErrors
{
    public static Error NotFound(Guid bookingId) =>
        Error.NotFound(
            "Booking.NotFound",
            $"The booking with the identifier {bookingId} was not found");

    public static readonly Error InvalidPeriod = new(
        "Booking.InvalidPeriod",
        "The booking start period must be before the end period",
        ErrorType.Validation);

    public static readonly Error NegativeBasePrice = new(
        "Booking.NegativeBasePrice",
        "The booking base price cannot be negative",
        ErrorType.Validation);

    public static readonly Error BasePriceOverflow = new(
        "Booking.BasePriceOverflow",
        "The calculated booking base price exceeds the supported amount",
        ErrorType.Validation);

    public static readonly Error NotPendingForConfirmation = Error.Conflict(
        "Booking.NotPendingForConfirmation",
        "Only pending bookings can be confirmed");

    public static readonly Error NotCancellable = Error.Conflict(
        "Booking.NotCancellable",
        "Only pending or confirmed bookings can be cancelled");

    public static readonly Error NotConfirmedForCompletion = Error.Conflict(
        "Booking.NotConfirmedForCompletion",
        "Only confirmed bookings can be completed");

    public static readonly Error ExtraCanOnlyBeAddedWhilePending = Error.Conflict(
        "Booking.ExtraCanOnlyBeAddedWhilePending",
        "Extras can only be added while the booking is pending");

    public static readonly Error NegativeExtraPrice = new(
        "Booking.NegativeExtraPrice",
        "The booking extra price cannot be negative",
        ErrorType.Validation);

    public static Error DuplicateExtraType(BookingExtraType type) =>
        Error.Conflict(
            "Booking.DuplicateExtraType",
            $"An extra of type {type} already exists on the booking");

    public static Error ExtraPriceNotConfigured(BookingExtraType type) =>
        Error.NotFound(
            "Booking.ExtraPriceNotConfigured",
            $"No price is configured for the booking extra type {type}");

    public static readonly Error ExtraCanOnlyBeRemovedWhilePending = Error.Conflict(
        "Booking.ExtraCanOnlyBeRemovedWhilePending",
        "Extras can only be removed while the booking is pending");

    public static Error ExtraNotFound(Guid bookingExtraId) =>
        Error.NotFound(
            "Booking.ExtraNotFound",
            $"The booking extra with the identifier {bookingExtraId} was not found");
}
