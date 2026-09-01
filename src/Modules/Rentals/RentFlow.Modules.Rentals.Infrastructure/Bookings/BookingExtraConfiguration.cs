using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentFlow.Modules.Rentals.Domain.Booking;
using RentFlow.Modules.Rentals.Infrastructure.Database;

namespace RentFlow.Modules.Rentals.Infrastructure.Bookings;

internal sealed class BookingExtraConfiguration : IEntityTypeConfiguration<BookingExtra>
{
    public void Configure(EntityTypeBuilder<BookingExtra> builder)
    {
        builder.ToTable("booking_extras", Schemas.Rentals);

        builder.HasKey(extra => extra.Id);

        builder.Property(extra => extra.Id)
            .ValueGeneratedNever();

        builder.Property(extra => extra.Type)
            .IsRequired();

        builder.Property(extra => extra.Price)
            .IsRequired();
    }
}
