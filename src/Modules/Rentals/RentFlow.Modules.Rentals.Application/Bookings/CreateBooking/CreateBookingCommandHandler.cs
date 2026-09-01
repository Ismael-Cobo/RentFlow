using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Rentals.Application.Abstractions.Data;
using RentFlow.Modules.Rentals.Domain.Booking;
using RentFlow.Modules.Rentals.Domain.Customer;
using RentFlow.Modules.Rentals.Domain.RentalVehicle;

namespace RentFlow.Modules.Rentals.Application.Bookings.CreateBooking;

internal sealed class CreateBookingCommandHandler(
    ICustomerRepository customerRepository,
    IRentalVehicleRepository rentalVehicleRepository,
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateBookingCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateBookingCommand request,
        CancellationToken cancellationToken)
    {
        Customer? customer = await customerRepository.GetCustomerAsync(
            request.CustomerId,
            cancellationToken);

        if (customer is null)
        {
            return Result.Failure<Guid>(CustomerErrors.NotFound(request.CustomerId));
        }

        RentalVehicle? rentalVehicle = await rentalVehicleRepository.GetRentalVehicleAsync(
            request.VehicleId,
            cancellationToken);

        if (rentalVehicle is null)
        {
            return Result.Failure<Guid>(RentalVehicleErrors.NotFound(request.VehicleId));
        }

        if (!rentalVehicle.IsAvailable)
        {
            return Result.Failure<Guid>(RentalVehicleErrors.Unavailable(request.VehicleId));
        }

        Result<int> pricingResult = BookingPricingPolicy.CalculateBasePrice(
            rentalVehicle.DailyPrice,
            request.StartPeriod,
            request.EndPeriod);

        if (pricingResult.IsFailure)
        {
            return Result.Failure<Guid>(pricingResult.Error);
        }

        Result<Booking> bookingResult = Booking.Create(
            Guid.CreateVersion7(),
            customer.Id,
            rentalVehicle.Id,
            request.StartPeriod,
            request.EndPeriod,
            pricingResult.Value);

        if (bookingResult.IsFailure)
        {
            return Result.Failure<Guid>(bookingResult.Error);
        }

        Booking booking = bookingResult.Value;

        bookingRepository.Insert(booking);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return booking.Id;
    }
}
