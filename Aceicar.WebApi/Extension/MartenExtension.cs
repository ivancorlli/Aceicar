using Marten;
using Marten.Events;
using Marten.Services.Json;
using Weasel.Core;
using Wolverine.Marten;

namespace Aceicar.WebApi.Extension;

public static class MartenExtension
{
    public static IServiceCollection InstallMarten(this IServiceCollection services,IConfiguration configuration)
    {
                // MARTEN DATABASE
        services.AddMarten(x =>
        {
            x
                // You have to specify a connection string for "administration"
                // with rights to provision new databases on the fly
                .MultiTenantedWithSingleServer(configuration.GetConnectionString("MessageDb")!)

                // You can map multiple tenant ids to a single named database
                .WithTenants("usercontextdb").InDatabaseNamed("UserContextDb")

                // Just declaring that there are additional tenant ids that should
                // have their own database
                .WithTenants("notificationcontextdb").InDatabaseNamed("NotificationContextDb"); // own database    

            x.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
            x.Events.StreamIdentity = StreamIdentity.AsGuid;
            x.UseDefaultSerialization(
                serializerType: SerializerType.Newtonsoft,
                // Optionally override the enum storage
                enumStorage: EnumStorage.AsString,
                // Optionally override the member casing
                casing: Casing.CamelCase
            );
        })
        .IntegrateWithWolverine(configuration.GetConnectionString("MessageDb")!)
        .UseLightweightSessions()
        .ApplyAllDatabaseChangesOnStartup()
        .AddAsyncDaemon(Marten.Events.Daemon.Resiliency.DaemonMode.HotCold)
        ;
        return services;
    }   
}