using RentFlow.Common.Domain;
using RentFlow.Modules.Identity.Domain.User.DomainEvent;

namespace RentFlow.Modules.Identity.Domain.User;

public sealed class User : Entity
{
    private User() { }

    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    public static User Create(
        string email,
        string firstName,
        string lastName,
        string passwordHash)
    {
        var newUser = new User
        {
            Id = Guid.CreateVersion7(),
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow
        };

        newUser.Raise(new UserRegisteredDomainEvent(
            newUser.Id,
            newUser.Email,
            newUser.FirstName,
            newUser.LastName));

        return newUser;
    }
}
