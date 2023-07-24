using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Application.ViewModel;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using Common.Basis.ValueObject;
using UserContext.Application.Interface;
using Marten;
using UserContext.Application.Error;
using UserContext.Application.Feature.ApplicationUser.Dto;

namespace UserContext.Application.Feature.User.Command.CreateUserWithProvider;

public sealed record CreateUserWithProviderCommand(
    string Email,
    string TimeZone
);
public sealed class CreateUserWithProviderHandler
{
    public static async Task<IOperationResult<CreateUserDto>> Handle(
        CreateUserWithProviderCommand command,
        IApplicationQuery query,
        IUoW session,
        UserManager manager,
        CancellationToken cancellationToken
    )
    {
        try{

        Email Email = Email.Create(command.Email);
        Guid userId = Guid.NewGuid();
        TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById(command.TimeZone);
        IReadOnlyList<UserProjection> userExists = await query.Query.Query<UserProjection>().Where(x=>x.Email == command.Email.Trim().ToLower()).ToListAsync();
        if (userExists.Count > 0) {
            return OperationResult<CreateUserDto>.Success(MapCreateUserDto.Map(userExists.First()));
        }
        UserCreated @event = new(userId,Email.Value,TimeZone.Id);
        Result<UserContext.Core.Aggregate.User> newUser = await manager.CreateUser(@event);
        if (newUser.IsFailure) return OperationResult<CreateUserDto>.Invalid(newUser.Error);
        session.UserRepository.Create(@event.UserId,@event);
        await session.UserRepository.Push(new UserCreatedEvent(@event.UserId,@event.Email,@event.TimeZone));
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult<CreateUserDto>.Create(MapCreateUserDto.Map(newUser.Value));
        } catch(Exception e)
        {
            return OperationResult<CreateUserDto>.Unexpected(UnexpectedError.Create(e.Message));
        }
    }

}