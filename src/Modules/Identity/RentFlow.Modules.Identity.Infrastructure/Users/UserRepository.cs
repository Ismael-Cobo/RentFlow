using Microsoft.EntityFrameworkCore;
using RentFlow.Modules.Identity.Domain.User;
using RentFlow.Modules.Identity.Infrastructure.Database;

namespace RentFlow.Modules.Identity.Infrastructure.Users;

internal sealed class UserRepository(IdentityDbContext dbContext) : IUserRepository
{
    private readonly IdentityDbContext _dbContext = dbContext;
    public async Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public void Insert(User user)
    {
        _dbContext.Users.Add(user);
    }
}
