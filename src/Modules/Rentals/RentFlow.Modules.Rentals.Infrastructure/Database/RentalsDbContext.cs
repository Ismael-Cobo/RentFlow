using Microsoft.EntityFrameworkCore;
using RentFlow.Modules.Rentals.Application.Abstractions.Data;
using RentFlow.Modules.Rentals.Domain.Booking;
using RentFlow.Modules.Rentals.Domain.Customer;
using RentFlow.Modules.Rentals.Domain.RentalVehicle;
using RentFlow.Modules.Rentals.Infrastructure.Bookings;
using RentFlow.Modules.Rentals.Infrastructure.Customers;
using RentFlow.Modules.Rentals.Infrastructure.RentalVehicles;

namespace RentFlow.Modules.Rentals.Infrastructure.Database;

public sealed class RentalsDbContext(DbContextOptions<RentalsDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Booking> Bookings { get; set; }

    internal DbSet<BookingExtra> BookingExtras { get; set; }

    internal DbSet<Customer> Customers { get; set; }

    internal DbSet<RentalVehicle> RentalVehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Rentals);
        modelBuilder.ApplyConfiguration(new BookingConfiguration());
        modelBuilder.ApplyConfiguration(new BookingExtraConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new RentalVehicleConfiguration());
    }
}
