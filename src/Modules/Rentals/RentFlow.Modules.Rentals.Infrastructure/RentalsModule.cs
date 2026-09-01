using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rentflow.Common.Infrastructure.Interceptors;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Modules.Rentals.Application.Abstractions.Data;
using RentFlow.Modules.Rentals.Application.Abstractions.Pricing;
using RentFlow.Modules.Rentals.Domain.Booking;
using RentFlow.Modules.Rentals.Domain.Customer;
using RentFlow.Modules.Rentals.Domain.RentalVehicle;
using RentFlow.Modules.Rentals.Infrastructure.Bookings;
using RentFlow.Modules.Rentals.Infrastructure.Customers;
using RentFlow.Modules.Rentals.Infrastructure.Database;
using RentFlow.Modules.Rentals.Infrastructure.Pricing;
using RentFlow.Modules.Rentals.Infrastructure.RentalVehicles;
using RentFlow.Modules.Rentals.Presentation.Customer;

namespace RentFlow.Modules.Rentals.Infrastructure;

public static class RentalsModule
{
    public static IServiceCollection AddRentalsModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }
    
    
    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<UserRegisteredIntegrationEventConsumer>();
    }

    private static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<RentalsDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Rentals))
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IRentalVehicleRepository, RentalVehicleRepository>();
        services.AddScoped<IBookingExtraPriceProvider, BookingExtraPriceProvider>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<RentalsDbContext>());
    }
}
