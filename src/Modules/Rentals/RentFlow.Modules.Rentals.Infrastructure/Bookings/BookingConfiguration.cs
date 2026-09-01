using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentFlow.Modules.Rentals.Domain.Booking;
using RentFlow.Modules.Rentals.Infrastructure.Database;

namespace RentFlow.Modules.Rentals.Infrastructure.Bookings;

internal sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings", Schemas.Rentals);

        builder.HasKey(booking => booking.Id);

        builder.Property(booking => booking.Id)
            .ValueGeneratedNever();

        builder.Property(booking => booking.CustomerId)
            .IsRequired();

        builder.HasIndex(booking => booking.CustomerId);

        builder.Property(booking => booking.VehicleId)
            .IsRequired();

        builder.Property(booking => booking.StartPeriod)
            .IsRequired();

        builder.Property(booking => booking.EndPeriod)
            .IsRequired();

        builder.Property(booking => booking.Status)
            .IsRequired();

        builder.Property(booking => booking.BasePrice)
            .IsRequired();

        builder.Property(booking => booking.TotalPrice)
            .IsRequired();

        builder.Property(booking => booking.CreatedAt)
            .IsRequired();

        builder.HasMany(booking => booking.Extras)
            .WithOne()
            .HasForeignKey("BookingId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(booking => booking.Extras)
            .HasField("_extras")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Ignore(booking => booking.DomainEvents);
    }
}
