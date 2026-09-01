using Microsoft.EntityFrameworkCore;
using RentFlow.Modules.Fleet.Infrastructure.Database;
using RentFlow.Modules.Identity.Infrastructure.Database;
using RentFlow.Modules.Rentals.Infrastructure.Database;

namespace Rentflow.Api.Extensions;

internal static class MigrationExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<IdentityDbContext>(scope);
        ApplyMigration<FleetDbContext>(scope);
        ApplyMigration<RentalsDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        context.Database.Migrate();
    }
}
