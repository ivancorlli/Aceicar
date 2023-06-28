using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.CreateUser;

public sealed record CreateUserCommand(string Email);


public sealed class CreateUserHandler
{


    public static async Task<OperationResult> Handle(CreateUserCommand command, IUoW _session, UserManager _manager,CancellationToken cancellationToken)
    {
        Email Email = Email.Create(command.Email);
        UserCreated @event = new(UserId.Create().Value, Email.Value);
        Result<User> newUser = await _manager.CreateUser(@event);
        if (newUser.IsSuccess)
        {
            User user = newUser.Value;
            _session.UserRepository.Create(user.Id,@event);
            await _session.SaveChangesAsync(cancellationToken);
            return new SuccessResult();
        }
        else return new InvalidResult(newUser.Error.Message);

    }

}