namespace RentFlow.Modules.Identity.Domain.User;

public interface IUserRepository
{
    Task<User?> GetUserAsync(Guid userId, CancellationToken cancellationToken);
    void Insert(User user);
}
