using Marten.Events.Aggregation;
using UserContext.Application.ViewModel;
using UserContext.Core.Enumerable;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.ValueObject;

namespace UserContext.Infrastructure.Projection.UserAccountProjection;

public class UserAccountProjection : SingleStreamProjection<UserAccount>
{
    public UserAccountProjection(){}
    public UserAccount Create(UserCreated @event)
    {
        DateTimeOffset time = DateTimeOffset.UtcNow;
        return new UserAccount()
        {
            Id = @event.UserId,
            Email = @event.Email,
            Status = @event.Status,
            CreatedAt = time,
            UpdatedAt = time
        };
    }
    public void Apply(EmailChanged @event, UserAccount user)
    {
        user.Email = @event.Email;
        user.Update();
    }

    public void Apply(UsernameChanged @event, UserAccount user)
    {
        user.Username = @event.Username;
        user.Update();
    }

    public void Apply(PhoneChanged @event, UserAccount user)
    {
        user.Phone = Phone.Create(@event.PhoneCountry,@event.PhoneNumber);
        user.Update();
    }

    public void Apply(UserSuspended @event, UserAccount user)
    {
        user.Status = @event.Status;
        user.Update();
    }



}