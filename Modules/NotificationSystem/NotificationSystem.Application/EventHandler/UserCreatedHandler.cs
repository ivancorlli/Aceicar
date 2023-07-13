using Common.IntegrationEvents;
using NotificationSystem.Core.Aggregate;
using NotificationSystem.Core.Event.UserEvent;
using NotificationSystem.Core.Repository;

namespace NotificationSystem.Application.EventHandler;

public static class UserCreatedHandler
{
    public static async void Handle(
        UserCreatedEvent command,
        IUoW session,
        CancellationToken cancellationToken
        )
    {
        UserCreated @event = new(command.UserId,command.Email,command.TimeZone);
        User user = new(@event);
        session.UserRepository.Create(user.Id,@event);
        await session.SaveChangesAsync(cancellationToken);
    }
    
}