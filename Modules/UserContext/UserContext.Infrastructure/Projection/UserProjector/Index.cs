using Marten.Events.Aggregation;
using UserContext.Application.ViewModel;
using UserContext.Core.Event.UserEvent;
namespace UserContext.Infrastructure.Projection.UserAccountProjection;

public class UserProjector : SingleStreamProjection<UserProjection>
{
    public UserProjector()
    {
    }
    public UserProjection Create(UserCreated @event)
    {
        DateTimeOffset time = DateTimeOffset.UtcNow;
        return new UserProjection()
        {
            Id = @event.UserId,
            Email = @event.Email,
            Status = @event.Status,
            CreatedAt = time,
            UpdatedAt = time,
            TimeZone = @event.TimeZone
        };
    }
    public void Apply(EmailChanged @event, UserProjection user)
    {
        user.Email = @event.Email;
        user.Update();
    }

    public void Apply(UsernameChanged @event, UserProjection user)
    {
        user.Username = @event.Username;
        user.Update();
    }

    public void Apply(PhoneChanged @event, UserProjection user)
    {
        user.PhoneNumber = @event.PhoneNumber;
        user.PhoneCountry = @event.PhoneCountry;
        user.Update();
    }

    public void Apply(UserSuspended @event, UserProjection user)
    {
        user.Status = @event.Status;
        user.Update();
    }

    public void Apply(ImageChanged @event, UserProjection user)
    {
        user.Picture = @event.Image;
        user.Update();
    }

    public void Apply(ProfileModified @event, UserProjection user)
    {
        user.Name = @event.Name;
        user.Surname = @event.Surname;
        user.Gender = @event.Gender;
        user.Birth = @event.Birth;
        user.Update();
    }

    public void Apply(LocationModified @event, UserProjection user)
    {
        user.Country = @event.Country;
        user.City = @event.City;
        user.State = @event.State;
        user.PostalCode = @event.PostalCode;
        if(@event.Street != null && @event.StreetNumber != null)
        {
            user.Street = @event.Street;
            user.StreetNumber = @event.StreetNumber;
        }
        user.Update();
    }
    public void Apply(TimeZoneModified @event, UserProjection user)
    {
        user.TimeZone = @event.TimeZone;
        user.Update();
    }
}