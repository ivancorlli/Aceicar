using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using Common.Basis.ValueObject;

namespace UserContext.Application.Feature.User.Command.ChangeEmail;

public sealed record ChangeEmailCommand(Guid UserId,string Email);

public sealed class ChangeEmailHandler
{
    public static async Task<IOperationResult> Handle(
        ChangeEmailCommand command,
        IUoW _session,
        UserManager _manager,
        CancellationToken cancellationToken
    ) 
    {
        Email Email = Email.Create(command.Email);
        Result<UserContext.Core.Aggregate.User> result = await _manager.ChangeEmail(command.UserId,Email);
        if(result.IsFailure) return OperationResult.Invalid(result.Error);
        _session.UserRepository.Apply(result.Value);
        await _session.SaveChangesAsync(cancellationToken);
        await _session.UserRepository.Push(new UserEmailChangedEvent(result.Value.Id,result.Value.Email.Value));
        return OperationResult.Success();
    }  
}