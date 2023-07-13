using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Application.Service;
using UserContext.Application.ViewModel;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using Common.Basis.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.CreateUserWithProvider;

public sealed record CreateUserWithProviderCommand(
    string Email,
    string TimeZone
);
public sealed class CreateUserWithProviderHandler
{
    public static async Task<IOperationResult<Guid>> Handle(
        CreateUserWithProviderCommand command,
        IUserAccountService service,
        IUoW session,
        UserManager manager,
        CancellationToken cancellationToken
    )
    {
        Email Email = Email.Create(command.Email);
        Guid userId = Guid.NewGuid();
        TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById(command.TimeZone);
        UserAccount? userExists = await service.FindByEmail(Email);
        if (userExists != null) {
            userId = userExists.Id;
            return OperationResult<Guid>.Success(userId);
        }
        UserCreated @event = new(userId,Email.Value,TimeZone.Id);
        Result<User> newUser = await manager.CreateUser(@event);
        if (newUser.IsFailure) return OperationResult<Guid>.Invalid(newUser.Error);
        session.UserRepository.Create(@event.UserId,@event);
        await session.SaveChangesAsync(cancellationToken);
        await session.UserRepository.Push(new UserCreatedEvent(@event.UserId,@event.Email,@event.TimeZone));
        return OperationResult<Guid>.Success(userId);
    }

}