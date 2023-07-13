using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Enumerable;
using UserContext.Core.Error;
using UserContext.Core.Repository;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.LocationModified;


public sealed record ModifyLocationCommand(string UserId,string Country,string City,string State,string PostalCode);
public static class ModifyLocationHandler
{
    public static async Task<IOperationResult> Handle(
        ModifyLocationCommand command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        Location location = Location.Create(command.Country,command.City,command.State,command.PostalCode,LocationStatus.Active);
        User? user = await session.UserRepository.FindById(Guid.Parse(command.UserId));
        if(user == null) return OperationResult.NotFound(new UserNotFound());
        user.ModifyLocation(location);
        session.UserRepository.Apply(user);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}