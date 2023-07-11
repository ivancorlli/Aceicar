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
        Guid UserId = Guid.Parse(command.UserId);
        User? user = await _session.UserRepository.FindById(UserId);
        if(user == null) return OperationResult.Invalid(new UserNotFound());
        user.SuspendUser();
        _session.UserRepository.Apply(user);
        return OperationResult.Success();
    }

}