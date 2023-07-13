using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Core.Aggregate;
using UserContext.Core.Error;
using UserContext.Core.Repository;

namespace UserContext.Application.Feature.ApplicationUser.Command.ModifyTimeZone;

public sealed record ModifyTimeZoneCommand(string UserId,string TimeZone);
public static class ModifyTimeZoneHandler
{
    public static async Task<IOperationResult> Handle(
        ModifyTimeZoneCommand command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        Guid UserId = Guid.Parse(command.UserId);
        TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById(command.TimeZone);
        User? user = await session.UserRepository.FindById(UserId);
        if(user == null) return OperationResult.NotFound(new UserNotFound());
        user.ModifyTimeZone(TimeZone);
        session.UserRepository.Apply(user);
        await session.SaveChangesAsync(cancellationToken);
        await session.UserRepository.Push(new UserTimeZoneModifiedEvent(user.Id,user.TimeZone.Id));
        return OperationResult.Success();
    }
}