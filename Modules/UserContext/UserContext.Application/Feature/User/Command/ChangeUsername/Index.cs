using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.User.Command.ChangeUsername;

public sealed record ChangeUsernameCommand(Guid UserId, string Username);


public sealed class ChangeUsernameHandler
{
    public static async Task<IOperationResult> Handle(
        ChangeUsernameCommand command,
        IUoW _session,
        UserManager _manager,
        CancellationToken cancellationToken
        )
    {
        Username Username = Username.Create(command.Username);
        Result<UserContext.Core.Aggregate.User> result = await _manager.ChangeUsername(command.UserId,Username);
        if(result.IsFailure) return OperationResult.Invalid(result.Error);
        _session.UserRepository.Apply(result.Value);
        await _session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }

}