using RentFlow.Common.Application.Messaging;
using RentFlow.Common.Domain;
using RentFlow.Modules.Identity.Application.Abstractions.Data;
using RentFlow.Modules.Identity.Domain.User;

namespace RentFlow.Modules.Identity.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
: ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.Email, request.FirstName, request.LastName, request.Password);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
