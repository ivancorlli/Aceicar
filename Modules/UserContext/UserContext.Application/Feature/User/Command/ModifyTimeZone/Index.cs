using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Core.Error;
using UserContext.Core.Repository;

namespace UserContext.Application.Feature.User.Command.ModifyTimeZone;

public sealed record ModifyTimeZoneCommand(Guid UserId,string TimeZone);
public static class ModifyTimeZoneHandler
{
    public static async Task<IOperationResult> Handle(
        ModifyTimeZoneCommand command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById(command.TimeZone);
        UserContext.Core.Aggregate.User? user = await session.UserRepository.FindById(command.UserId);
        if(user == null) return OperationResult.NotFound(new UserNotFound());
        user.ModifyTimeZone(TimeZone);
        session.UserRepository.Apply(user);
        await session.SaveChangesAsync(cancellationToken);
        await session.UserRepository.Push(new UserTimeZoneModifiedEvent(user.Id,user.TimeZone.Id));
        return OperationResult.Success();
    }
}