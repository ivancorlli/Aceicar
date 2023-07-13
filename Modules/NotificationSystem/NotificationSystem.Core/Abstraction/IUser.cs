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

    public IUser ChangeEmail(Email email,string code)
    {
        EmailChanged @event = new(Id, email.Value,code);
        Apply(@event);
        Raise(@event);
        return this;
    }


    public bool VerifyEmail(string code)
    {
        Email.Verify(code);
        if (Email.Verified)
        {
            EmailVerified @event = new(Id,Email.Value,code);
            Apply(@event);
            Raise(@event);
            return Email.Verified;
        }
        else
        {
            return false;
        }
    }

    public IUser ChangePhone(Phone phone,string code)
    {
        PhoneChanged @event = new(Id, phone.Country, phone.Number,code);
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
                PhoneVerified @event = new(Id,Phone.Number,code);
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

    public IUser ChangeProfile(Profile profile)
    {
        ProfileChanged @event = new(Id, profile.Name, profile.Surname, profile.Gender);
        Apply(@event);
        Raise(@event);
        return this;
    }

    public IUser ModifyTimeZone(TimeZoneInfo timeZone)
    {
        TimeZoneModified @event = new(Id, timeZone.Id);
        Apply(@event);
        Raise(@event);
        return this;
    }
    private void Apply(EmailChanged @event)
    {
        Email = Email.Create(@event.Email,@event.VerificationCode);
    }

    private void Apply(EmailVerified @event)
    {
        Email.Verify(@event.VerificationCode);
    }

    private void Apply(PhoneChanged @event)
    {
        Phone = Phone.Create(@event.Country, @event.Number,@event.VerificationCode);
    }

    private void Apply(PhoneVerified @event)
    {
        if (Phone != null) Phone.Verify(@event.VerificationCodeCode);
    }

    private void Apply(ProfileChanged @event)
    {
        Profile = Profile.Create(@event.Name, @event.Surname, @event.Gender);
    }
    private void Apply(TimeZoneModified @event)
    {
       TimeZone = TimeZoneInfo.FindSystemTimeZoneById(@event.TimeZone);
    }

}