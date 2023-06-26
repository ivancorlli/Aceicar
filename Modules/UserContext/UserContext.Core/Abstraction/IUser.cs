using System.Security.Cryptography.X509Certificates;
using Common.Basis.Aggregate;
using UserContext.Core.Enumerable;
using UserContext.Core.Event;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Abstraction;

public abstract class IUser : IAggregate
{
    public Guid Id {get; protected set;} = default!;
    public Email Email { get; private set; } = default!;
    public Status Status { get; private set; }
    public Username? Username { get; private set; }
    public Phone? Phone { get; protected set; }
    public ProfileImage? Image { get; protected set; }
    public Profile? Profile { get; protected set; }
    public Location? Location { get; protected set; }

    protected IUser(Email email)
    {
        UserCreated @event = new(UserId.Create().Value, email, Status.Active);
        Apply(@event);
        Raise(@event);
    }

    public IUser SuspendUser()
    {
        UserSuspended @event = new(Id, Status.Suspended);
        Apply(@event);
        Raise(@event);
        return this;
    }

    public IUser ChangeEmail(Email email)
    {
        EmailChanged @event = new(Id, email);
        Apply(@event);
        Raise(@event);
        return this;
    }

    internal IUser ChangePhone(Phone phone)
    {
        PhoneChanged @event = new(Id, phone);
        Apply(@event);
        Raise(@event);
        return this;
    }

    internal IUser ChangeUsername(Username username)
    {
        UsernameChanged @event = new(Id, username);
        Apply(@event);
        Raise(@event);
        return this;
    }

    public IUser ChangeImage(ProfileImage image)
    {
        ImageChanged @event = new(Id, image);
        Apply(@event);
        Raise(@event);
        return this;
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
    public void Apply(UsernameChanged @event)
    {
        Username = @event.Username;
        UpdateVersion();
    }

}