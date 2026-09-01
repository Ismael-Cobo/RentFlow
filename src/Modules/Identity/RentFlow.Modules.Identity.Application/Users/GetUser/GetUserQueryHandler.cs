using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Identity.Domain.User;

namespace RentFlow.Modules.Identity.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetUserAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
        }

        var userResponse = new UserResponse(
            Id: user.Id,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName
        );
        
        return Result.Success(userResponse);
    }
}
