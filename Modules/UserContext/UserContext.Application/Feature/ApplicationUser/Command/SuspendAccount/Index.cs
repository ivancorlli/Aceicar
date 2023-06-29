using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Error;
using UserContext.Core.Repository;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.SuspendAccount;

public sealed record SuspendAccountCommand(string UserId);

public sealed class Index
{

    public static async Task<IOperationResult> Handle(
        SuspendAccountCommand command,
        IUoW _session,
        CancellationToken cancellationToken

    )
    {
        UserId UserId = new(command.UserId);
        User? user = await _session.UserRepository.FindById(UserId.Value);
        if(user == null) return OperationResult.Invalid(new UserNotFound().Message);
        user.SuspendUser();
        _session.UserRepository.Apply(user);
        await _session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }

}