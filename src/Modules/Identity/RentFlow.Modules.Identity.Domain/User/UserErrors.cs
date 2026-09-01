using RentFlow.Common.Domain;

namespace RentFlow.Modules.Identity.Domain.User;

public static class UserErrors
{
        public static Error NotFound(Guid userId) =>
            Error.NotFound("User.NotFound", $"The user with the identifier {userId} was not found");
}
