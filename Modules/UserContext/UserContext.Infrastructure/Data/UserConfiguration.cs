using Marten;
using Marten.Events.Projections;
using UserContext.Application.ViewModel;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Infrastructure.Projection.UserAccountProjection;

namespace UserContext.Infrastructure.Data;

public static class UserConfiguration
{
    public  static StoreOptions ConfigureUser(this StoreOptions opts)
    {
        opts.Schema.For<User>().Identity(x=>x.Id).MultiTenanted();
        opts.Schema.For<UserAccount>().Identity(x=>x.Id).MultiTenanted();
        // Register events
        opts.Events.AddEventType(typeof(UserCreated));
        opts.Events.AddEventType(typeof(EmailChanged));
        opts.Events.AddEventType(typeof(PhoneChanged));
        opts.Events.AddEventType(typeof(UsernameChanged));
        opts.Events.AddEventType(typeof(UserSuspended));
        opts.Events.AddEventType(typeof(ImageChanged));
        opts.Events.AddEventType(typeof(ProfileModified));
        opts.Events.AddEventType(typeof(LocationModified));
        opts.Events.AddEventType(typeof(TimeZoneModified));
        // Projections
        opts.Projections.LiveStreamAggregation<User>().MultiTenanted();
        opts.Projections.Add<UserAccountProjection>(ProjectionLifecycle.Inline);
        return opts;
    }
}
