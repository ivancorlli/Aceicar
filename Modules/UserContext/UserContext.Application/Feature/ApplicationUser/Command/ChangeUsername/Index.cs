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
    public static async Task<OperationResult> Handle(
        ChangeUsernameCommand command,
        IUoW _session,
        UserManager _manager,
        CancellationToken cancellationToken
        )
    {
        UserId UserId = new(command.UserId);
        Username Username = Username.Create(command.Username);
        Result<User> result = await _manager.ChangeUsername(UserId,Username);
        if(result.IsFailure) return new InvalidResult(result.Error.Message);
        _session.UserRepository.Apply(result.Value);
        await _session.SaveChangesAsync(cancellationToken);
        return new SuccessResult();
    }

}