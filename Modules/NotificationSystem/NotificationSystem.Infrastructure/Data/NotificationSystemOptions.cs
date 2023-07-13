using Marten;
using Marten.Services.Json;
using NotificationSystem.Core.Aggregate;
using NotificationSystem.Core.Event.UserEvent;
using Weasel.Core;

namespace NotificationSystem.Infrastructure.Data;

public static class NotificationSystemOptions
{

    public static StoreOptions NotificationSystem(this StoreOptions options)
    {
        options.UseDefaultSerialization(
        serializerType: SerializerType.SystemTextJson,
        // Optionally override the enum storage
        enumStorage: EnumStorage.AsString,
        // Optionally override the member casing
        casing: Casing.CamelCase
        );
        options.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.CreateOrUpdate;
        options.Schema.For<User>().Identity(x => x.Id).SingleTenanted();
        // Events
        options.Events.AddEventType(typeof(UserCreated));
        options.Events.AddEventType(typeof(EmailChanged));
        options.Events.AddEventType(typeof(PhoneChanged));
        options.Events.AddEventType(typeof(ProfileChanged));
        options.Events.AddEventType(typeof(TimeZoneModified));
        // Projections
        options.Projections.LiveStreamAggregation<User>().SingleTenanted();
        return options;
    }
}