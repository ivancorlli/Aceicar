using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;
using Wolverine;

namespace UserContext.Application.Feature.ApplicationUser.Command.CreateUser;

public sealed record CreateUserCommand(string Email, string TimeZone);


public sealed class CreateUserHandler
{


    public static async Task<IOperationResult> Handle(
        CreateUserCommand command, 
        IUoW _session, 
        UserManager _manager, 
        IMessageBus _bus,
        CancellationToken cancellationToken
        )
    {
        Email Email = Email.Create(command.Email);
        Guid UserId = Guid.NewGuid();
        TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById(command.TimeZone);
        UserCreated @event = new(UserId, Email.Value, TimeZone.Id);
        Result<User> newUser = await _manager.CreateUser(@event);
        if (newUser.IsSuccess)
        {
            User user = newUser.Value;
            _session.UserRepository.Create(user.Id, @event);
            await _bus.PublishAsync(new UserCreatedEvent(@event.UserId, @event.Email));
            await _session.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }
        else return OperationResult.Invalid(newUser.Error);

    }

}