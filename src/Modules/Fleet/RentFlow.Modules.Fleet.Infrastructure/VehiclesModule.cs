using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rentflow.Common.Infrastructure.Interceptors;
using RentFlow.Common.Presentation.Endpoints;
using RentFlow.Modules.Fleet.Application.Abstractions.Data;
using RentFlow.Modules.Fleet.Domain.Vehicle;
using RentFlow.Modules.Fleet.Infrastructure.Database;
using RentFlow.Modules.Fleet.Infrastructure.Vehicles;

namespace RentFlow.Modules.Fleet.Infrastructure;

public static class VehiclesModule
{
    public static IServiceCollection AddVehiclesModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);

        services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return services;
    }

    private static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FleetDbContext>((sp, options) =>
            options
                .UseNpgsql(
                    configuration.GetConnectionString("Database"),
                    npgsqlOptions => npgsqlOptions
                        .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Vehicles))
                .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>())
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IVehicleRepository, VehicleRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<FleetDbContext>());
    }
}
