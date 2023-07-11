using Common.IntegrationEvents;
using NotificationSystem.Core.Aggregate;
using NotificationSystem.Core.Event.UserEvent;
using NotificationSystem.Core.Repository;

namespace NotificationSystem.Application.EventHandler;

public static class UserCreatedHandler
{
    public static void Handle(
        UserCreatedEvent command,
        IUoW session
        )
    {
        UserCreated @event = new(command.UserId,command.Email);
        User user = new(@event);
        session.UserRepository.Create(user.Id,@event);
    }
    
}