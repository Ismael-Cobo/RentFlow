using Microsoft.EntityFrameworkCore;
using RentFlow.Modules.Identity.Application.Abstractions.Data;
using RentFlow.Modules.Identity.Domain.User;

namespace RentFlow.Modules.Identity.Infrastructure.Database;

public sealed class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
    }
}
