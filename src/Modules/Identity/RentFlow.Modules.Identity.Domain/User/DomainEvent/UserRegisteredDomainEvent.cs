namespace RentFlow.Modules.Identity.Domain.User.DomainEvent;

public sealed class UserRegisteredDomainEvent(
    Guid userId,
    string email,
    string firstName,
    string lastName) : Common.Domain.DomainEvent
{
    public Guid UserId { get; init; } = userId;
    public string Email { get; init; } = email;
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
}
