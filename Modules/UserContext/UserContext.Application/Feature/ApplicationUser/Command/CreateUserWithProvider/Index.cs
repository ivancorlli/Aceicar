using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Application.Service;
using UserContext.Application.ViewModel;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.CreateUserWithProvider;

public sealed record CreateUserWithProviderCommand(
    string Email
);
public sealed class CreateUserWithProviderHandler
{
    public static async Task<IOperationResult<UserId>> Handle(
        CreateUserWithProviderCommand command,
        IUserAccountService service,
        IUoW session,
        UserManager manager,
        CancellationToken cancellationToken
    )
    {
        Email Email = Email.Create(command.Email);
        UserId userId = UserId.Create();
        UserAccount? userExists = await service.FindByEmail(Email);
        if (userExists != null) {
            userId = new(userExists.Id);
            return OperationResult<UserId>.Success(userId);
        }
        UserCreated @event = new(userId.Value,Email.Value);
        Result<User> newUser = await manager.CreateUser(@event);
        if (newUser.IsFailure) return OperationResult<UserId>.Invalid(newUser.Error.Message);
        session.UserRepository.Create(@event.UserId,@event);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult<UserId>.Success(userId);
    }

}