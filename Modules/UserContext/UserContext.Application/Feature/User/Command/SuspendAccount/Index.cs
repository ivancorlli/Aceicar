using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Application.Feature.User.Dto;
using UserContext.Core.Error;
using UserContext.Core.Repository;

namespace UserContext.Application.Feature.User.Command.SuspendAccount;

public sealed record SuspendAccountCommand(Guid UserId);

public sealed class Index
{

    public static async Task<IOperationResult<SuspendAccountDto>> Handle(
        SuspendAccountCommand command,
        IUoW _session,
        CancellationToken cancellationToken

    )
    {
        UserContext.Core.Aggregate.User? user = await _session.UserRepository.FindById(command.UserId);
        if(user == null) return OperationResult<SuspendAccountDto>.Invalid(new UserNotFound());
        user.SuspendUser();
        _session.UserRepository.Apply(user);
        await _session.SaveChangesAsync(cancellationToken);
        return OperationResult<SuspendAccountDto>.Success(MapSuspendAccountDto.Map(user));
    }

}