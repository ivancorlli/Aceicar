using NotificationSystem.Core.Abstraction;
using NotificationSystem.Core.Event.UserEvent;
using NotificationSystem.Core.ValueObject;

namespace NotificationSystem.Core.Aggregate;

public class User : IUser
{
    public User(UserCreated @event)
    {
        Id = @event.Userid;
        this.Email = Email.CreateWithVerify(@event.Email);
    }
}