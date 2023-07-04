using UserContext.Core.Abstraction;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Aggregate;

public class User : IUser
{
    public static User Create(UserCreated @event)
    {
        User newUser = new User();
        newUser.Id = @event.UserId;
        newUser.Email = Email.Create(@event.Email);
        newUser.TimeZone = ValueObject.TimeZone.Create(@event.TimeZoneCountry,@event.TimeZone);
        newUser.Status = @event.Status;

        return newUser;
    }
}