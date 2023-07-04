using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.CreateUser;

public sealed record CreateUserCommand(string Email,string TimeZoneCountry,string TimeZone);


public sealed class CreateUserHandler
{


    public static async Task<IOperationResult> Handle(CreateUserCommand command, IUoW _session, UserManager _manager,CancellationToken cancellationToken)
    {
        Email Email = Email.Create(command.Email);
        UserId UserId = UserId.Create();
        UserCreated @event = new(UserId.Value, Email.Value,command.TimeZoneCountry,command.TimeZone);
        Result<User> newUser = await _manager.CreateUser(@event);
        if (newUser.IsSuccess)
        {
            User user = newUser.Value;
            _session.UserRepository.Create(user.Id,@event);
            await _session.SaveChangesAsync(cancellationToken);
            return OperationResult.Success();
        }
        else return OperationResult.Invalid(newUser.Error.Message);

    }

}