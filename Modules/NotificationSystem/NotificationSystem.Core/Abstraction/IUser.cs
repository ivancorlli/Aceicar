using Common.Basis.Aggregate;
using NotificationSystem.Core.Event.UserEvent;
using NotificationSystem.Core.ValueObject;

namespace NotificationSystem.Core.Abstraction;

public abstract class IUser : IAggregate
{
    public Email Email { get; protected set; } = default!;
    public TimeZoneInfo TimeZone { get; protected set; } = default!;
    public Phone? Phone { get; protected set; } = default!;
    public Profile? Profile { get; protected set; }

    public IUser ChangeEmail(Email email)
    {
        EmailChanged @event = new(Id, email.Value);
        Apply(@event);
        Raise(@event);
        return this;
    }


    public bool VerifyEmail(string code)
    {
        Email.Verify(code);
        if (Email.Verified)
        {
            EmailVerified @event = new(Id, code);
            Apply(@event);
            Raise(@event);
            return Email.Verified;
        }
        else
        {
            return false;
        }
    }

    public IUser ChangePhone(Phone phone)
    {
        PhoneChanged @event = new(Id, phone.Country, phone.Number);
        Apply(@event);
        Raise(@event);
        return this;
    }

    public bool VerifyPhone(string code)
    {
        if (Phone != null)
        {
            Phone.Verify(code);
            if (Phone.Verified)
            {
                PhoneVerified @event = new(Id, code);
                Apply(@event);
                Raise(@event);
                return Phone.Verified;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public IUser ModifyProfile(Profile profile)
    {
        ProfileModified @event = new(Id, profile.Name, profile.Surname, profile.Gender, profile.Birth);
        Apply(@event);
        Raise(@event);
        return this;
    }
    private void Apply(EmailChanged @event)
    {
        Email = Email.Create(@event.Email);
    }

    private void Apply(EmailVerified @event)
    {
        Email.Verify(@event.Code);
    }

    private void Apply(PhoneChanged @event)
    {
        Phone = Phone.Create(@event.Country, @event.Number);
    }

    private void Apply(PhoneVerified @event)
    {
        if (Phone != null) Phone.Verify(@event.Code);
    }

    private void Apply(ProfileModified @event)
    {
        Profile = Profile.Create(@event.Name,@event.Surname,@event.Gender,@event.Birth);
    }

}