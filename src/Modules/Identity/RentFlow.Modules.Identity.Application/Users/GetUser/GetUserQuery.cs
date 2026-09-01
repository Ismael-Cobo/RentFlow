using RentFlow.Common.Application.Messaging;

namespace RentFlow.Modules.Identity.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
