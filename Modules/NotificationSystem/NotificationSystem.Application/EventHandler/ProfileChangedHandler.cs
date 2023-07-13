using Common.IntegrationEvents;
using NotificationSystem.Core.Aggregate;
using NotificationSystem.Core.Repository;
using NotificationSystem.Core.ValueObject;

namespace NotificationSystem.Application.EventHandler;

public static class ProfileChangedHandler
{
    public static async void Handle(
        UserProfileChangedEvent command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        User? user = await session.UserRepository.FindById(command.UserId);
        if(user != null)
        {
            Profile profile = Profile.Create(command.Name,command.Surname,command.Gender);
            user.ChangeProfile(profile);
            session.UserRepository.Apply(user);
            await session.SaveChangesAsync(cancellationToken);
        }
    }
    
}