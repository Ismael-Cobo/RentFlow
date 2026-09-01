using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Identity.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password, string FirstName, string LastName)
    : ICommand<Guid>;
