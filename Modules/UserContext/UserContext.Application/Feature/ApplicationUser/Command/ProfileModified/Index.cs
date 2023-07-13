using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.IntegrationEvents;
using UserContext.Core.Aggregate;
using UserContext.Core.Constant;
using UserContext.Core.Error;
using UserContext.Core.Repository;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.ProfileModified;


public sealed record ModifyProfileCommand(string UserId,string Name,string Surname,string Gender,DateTime Birth);
public static class ModifyProfileHandler
{
    public static async Task<IOperationResult> Handle(
        ModifyProfileCommand command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        Guid userId = Guid.Parse(command.UserId);
        if(command.Gender != Gender.Male && command.Gender != Gender.Female) return OperationResult.Invalid(new InvalidGender());
        Profile profile = Profile.Create(command.Name,command.Surname,command.Gender,command.Birth);
        User? user = await session.UserRepository.FindById(userId);
        if(user == null) return OperationResult.NotFound(new UserNotFound());
        user.ModifyProfile(profile);
        session.UserRepository.Apply(user);
        await session.SaveChangesAsync(cancellationToken);
        profile = user.Profile!;
        await session.UserRepository.Push(new UserProfileChangedEvent(user.Id,profile.Name,profile.Surname,profile.Gender));
        return OperationResult.Success();         
    }
}