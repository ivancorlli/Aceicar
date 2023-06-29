using Marten;
using Marten.Events.Aggregation;
using Marten.Events.Projections;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;

namespace UserContext.Infrastructure.Data;

public static class UserConfiguration
{

    public static StoreOptions ConfigureUser(this StoreOptions options)
    {
        options.Schema.For<User>().Identity(x => x.Id);
        options.Schema.For<User>().Index(x => x.Id).UniqueIndex(x => x.Id);
        options.Schema.For<User>().Index(x => x.Email.Value).UniqueIndex(x => x.Email.Value);
        // Register events
        options.Events.AddEventType(typeof(UserCreated));
        options.Events.AddEventType(typeof(EmailChanged));
        options.Events.AddEventType(typeof(PhoneChanged));
        options.Events.AddEventType(typeof(UsernameChanged));
        options.Events.AddEventType(typeof(UserSuspended));
        options.Events.AddEventType(typeof(ImageChanged));
        options.Projections.Add<UserProjection>(ProjectionLifecycle.Async);
        return options;
    }

}


public record NewUser{
    public Guid Id {get;set;}
    public string Email {get;set;} = default!;
}

public class UserProjection : SingleStreamProjection<NewUser>
{
    public UserProjection(){}
    public NewUser Create(UserCreated @event){
        var user = new NewUser();
        user.Id =@event.UserId;
        user.Email =@event.Email;
        return user;
    }
}
