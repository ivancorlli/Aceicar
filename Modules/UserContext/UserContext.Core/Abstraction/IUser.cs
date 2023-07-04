using Common.Basis.Aggregate;
using UserContext.Core.Enumerable;
using UserContext.Core.Event.UserEvent;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Abstraction;

public abstract class IUser : IAggregate
{
    public Email Email { get; protected set; } = default!;
    public Status Status { get; protected set; }
    public Username? Username { get; private set; }
    public Phone? Phone { get; protected set; }
    public ProfileImage? Image { get; protected set; }
    public Profile? Profile { get; protected set; }
    public Location? Location { get; protected set; }

    public IUser SuspendUser()
    {
        UserSuspended @event = new(Id, Status.Suspended);
        Raise(@event);
        Apply(@event);
        return this;
    }

    public IUser ChangeEmail(Email email)
    {
        EmailChanged @event = new(Id, email.Value);
        Raise(@event);
        Apply(@event);
        return this;
    }

    internal IUser ChangePhone(Phone phone)
    {
        PhoneChanged @event = new(Id, phone.PhoneCountry,phone.PhoneNumber);
        Raise(@event);
        Apply(@event);
        return this;
    }

    internal IUser ChangeUsername(Username username)
    {
        UsernameChanged @event = new(Id, username.Value);
        Raise(@event);
        Apply(@event);
        return this;
    }

    public IUser ChangeImage(ProfileImage image)
    {
        ImageChanged @event = new(Id, image.Value);
        Raise(@event);
        Apply(@event);
        return this;
    }

    public IUser ModifyProfile(Profile profile)
    {
        ProfileModified @event = new(Id,profile.Name,profile.Surname,profile.Gender,profile.Birth);
        Raise(@event);
        Apply(@event);
        return this;
    }

    public IUser ModifyLocation(Location location)
    {
        LocationModified @event = new(Id,location.Country,location.City,location.State,location.PostalCode,location.Status);
        Raise(@event);
        Apply(@event);
        return this;
    }

    public void Apply(EmailChanged @event)
    {
        Email = Email.Create(@event.Email);
    }

    public void Apply(PhoneChanged @event)
    {
        Phone = Phone.Create(@event.PhoneCountry,@event.PhoneNumber);
    }

    public void Apply(ImageChanged @event)
    {
        Image =new ProfileImage(@event.Image);
    }

    public void Apply(UserSuspended @event)
    {
        Status = @event.Status;
    }
    public void Apply(UsernameChanged @event)
    {
        Username = Username.Create(@event.Username);
    }

    public void Apply(ProfileModified @event)
    {
        Profile = Profile.Create(@event.Name,@event.Surname,@event.Gender,@event.Birth);
    }

    public void Apply(LocationModified @event)
    {
        Location = Location.Create(@event.Country,@event.City,@event.State,@event.PostalCode,@event.Status);
    }
}