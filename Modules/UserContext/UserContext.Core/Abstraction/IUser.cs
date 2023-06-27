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

    public UserSuspended SuspendUser()
    {
        UserSuspended @event = new(Id, Status.Suspended);
        Apply(@event);
        return @event;
    }

    public EmailChanged ChangeEmail(Email email)
    {
        EmailChanged @event = new(Id, email.Value);
        Apply(@event);
        return @event;
    }

    internal PhoneChanged ChangePhone(Phone phone)
    {
        PhoneChanged @event = new(Id, phone.PhoneCountry,phone.PhoneNumber);
        Apply(@event);
        return @event;
    }

    internal UsernameChanged ChangeUsername(Username username)
    {
        UsernameChanged @event = new(Id, username.Value);
        Apply(@event);
        return @event;
    }

    public ImageChanged ChangeImage(ProfileImage image)
    {
        ImageChanged @event = new(Id, image.Value);
        Apply(@event);
        return @event;
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

}