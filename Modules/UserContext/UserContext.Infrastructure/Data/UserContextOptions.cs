using Marten;
using Marten.Events.Projections;
using Marten.Services.Json;
using UserContext.Application.ViewModel;
using UserContext.Core.Aggregate;
using UserContext.Core.Event.UserEvent;
using UserContext.Infrastructure.Projection.UserAccountProjection;
using Weasel.Core;

namespace UserContext.Infrastructure.Data;

public static class UserContextOptions
{
    public static StoreOptions UserContext(this StoreOptions option)
    {
        option.UseDefaultSerialization(
        serializerType: SerializerType.SystemTextJson,
        // Optionally override the enum storage
        enumStorage: EnumStorage.AsString,
        // Optionally override the member casing
        casing: Casing.CamelCase
        );
        option.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.CreateOrUpdate;
        option.Schema.For<User>().Identity(x => x.Id);
        option.Schema.For<UserProjection>().Identity(x => x.Id);
        // Register events
        option.Events.AddEventType(typeof(UserCreated));
        option.Events.AddEventType(typeof(EmailChanged));
        option.Events.AddEventType(typeof(PhoneChanged));
        option.Events.AddEventType(typeof(UsernameChanged));
        option.Events.AddEventType(typeof(UserSuspended));
        option.Events.AddEventType(typeof(ImageChanged));
        option.Events.AddEventType(typeof(ProfileModified));
        option.Events.AddEventType(typeof(LocationModified));
        option.Events.AddEventType(typeof(TimeZoneModified));
        // Projections
        option.Projections.LiveStreamAggregation<User>().Identity(x=>x.Id);
        option.Projections.Add<UserProjector>(ProjectionLifecycle.Inline);
        return option;
    }
}
