using Common.Basis.Aggregate;
using UserContext.Core.Enumerable;
using UserContext.Core.Event;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Abstraction;

public abstract class IUser : IAggregate<UserId>
{
    public Email Email { get; private set; } = default!;
    public Status Status { get; private set; }
    public Phone? Phone { get; protected set; }
    public ProfileImage? Image { get; protected set; }
    public Profile? Profile { get; protected set; }
    public Location? Location { get; protected set; }

    protected IUser(Email email)
    {
        UserCreated @event = new(UserId.Create(), email, Status.Active);
        Apply(@event);
        Raise(@event);
    }

    public void SuspendUser()
    {
        UserSuspended @event = new(Id, Status.Suspended);
        Apply(@event);
        Raise(@event);
    }

    public void ChangeEmail(Email email)
    {
        EmailChanged @event = new(Id, email);
        Apply(@event);
        Raise(@event);
    }

    public void ChangePhone(Phone phone)
    {
        PhoneChanged @event = new(Id, phone);
        Apply(@event);
        Raise(@event);
    }

    public void ChangeImage(ProfileImage image)
    {
        ImageChanged @event = new(Id, image);
        Apply(@event);
        Raise(@event);
    }


    public void Apply(UserCreated @event)
    {
        Id = @event.UserId;
        Email = @event.Email;
        Status = @event.Status;
        UpdateVersion();
    }

    public void Apply(EmailChanged @event)
    {
        Email = @event.Email;
        UpdateVersion();
    }

    public void Apply(PhoneChanged @event)
    {
        Phone = @event.Phone;
        UpdateVersion();
    }

    public void Apply(ImageChanged @event)
    {
        Image = @event.Image;
        UpdateVersion();
    }

    public void Apply(UserSuspended @event)
    {
        Status = @event.Status;
        UpdateVersion();
    }

}