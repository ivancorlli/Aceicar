using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using Common.Basis.ValueObject;
using UserContext.Application.Error;
using UserContext.Application.Feature.ApplicationUser.Dto;

namespace UserContext.Application.Feature.User.Command.CreateUser;

public sealed record CreateUserCommand(string Email, string TimeZone);


public sealed class CreateUserHandler
{


    public static async Task<IOperationResult<CreateUserDto>> Handle(
        CreateUserCommand command, 
        IUoW _session, 
        UserManager _manager, 
        CancellationToken cancellationToken
        )
    {
        try{

        Email Email = Email.Create(command.Email);
        Guid UserId = Guid.NewGuid();
        TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById(command.TimeZone);
        UserCreated @event = new(UserId, Email.Value, TimeZone.Id);
        Result<UserContext.Core.Aggregate.User> newUser = await _manager.CreateUser(@event);
        if (newUser.IsSuccess)
        {
            UserContext.Core.Aggregate.User user = newUser.Value;
            _session.UserRepository.Create(user.Id, @event);
            await _session.UserRepository.Push(new UserCreatedEvent(@event.UserId, @event.Email,@event.TimeZone));
            await _session.SaveChangesAsync(cancellationToken);
            return OperationResult<CreateUserDto>.Create(MapCreateUserDto.Map(user));
        }
        else return OperationResult<CreateUserDto>.Invalid(newUser.Error);
        }catch(Exception e)
        {
            return OperationResult<CreateUserDto>.Unexpected(UnexpectedError.Create(e.Message));
        }

    }

}