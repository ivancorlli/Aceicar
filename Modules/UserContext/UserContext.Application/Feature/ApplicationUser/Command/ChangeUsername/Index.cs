using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.ChangeUsername;

public sealed record ChangeUsernameCommand(string UserId, string Username);


public sealed class Index
{
    public static async Task<IOperationResult> Handle(
        ChangeUsernameCommand command,
        IUoW _session,
        UserManager _manager,
        CancellationToken cancellationToken
        )
    {
        Guid UserId = Guid.Parse(command.UserId);
        Username Username = Username.Create(command.Username);
        Result<User> result = await _manager.ChangeUsername(UserId,Username);
        if(result.IsFailure) return OperationResult.Invalid(result.Error);
        _session.UserRepository.Apply(result.Value);
        return OperationResult.Success();
    }

}