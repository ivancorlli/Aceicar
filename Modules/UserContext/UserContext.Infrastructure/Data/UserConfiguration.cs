using Marten;
using Marten.Events.Projections;
using UserContext.Application.ViewModel;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Infrastructure.Projection.UserAccountProjection;

namespace UserContext.Infrastructure.Data;

public static class UserConfiguration
{

    public static StoreOptions ConfigureUser(this StoreOptions options)
    {
        // Schema
        options.Schema.For<User>().Identity(x=>x.Id);
        options.Schema.For<UserAccount>().Identity(x=>x.Id);
        // Register events
        options.Events.AddEventType(typeof(UserCreated));
        options.Events.AddEventType(typeof(EmailChanged));
        options.Events.AddEventType(typeof(PhoneChanged));
        options.Events.AddEventType(typeof(UsernameChanged));
        options.Events.AddEventType(typeof(UserSuspended));
        options.Events.AddEventType(typeof(ImageChanged));
        // Projections
        options.Projections.LiveStreamAggregation<User>();
        options.Projections.Add<UserAccountProjection>(ProjectionLifecycle.Inline);
        return options;
    }

}
