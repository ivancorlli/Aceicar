using Marten;
using Marten.Events;
using Marten.Services.Json;
using Marten.Storage;
using Weasel.Core;
using Wolverine.Marten;

namespace Aceicar.WebApi.Extension;

public static class MartenExtension
{
    public static IServiceCollection InstallMarten(this IServiceCollection services, IConfiguration configuration)
    {
        // MARTEN DATABASE
        services.AddMarten(x =>
        {
            const string usercontext = "usercontextdb";
            const string notificationsystem = "notificationsystemdb";
            x.MultiTenantedDatabases(tenancy =>
            {   
            // You would probably be pulling the connection strings out of configuration,
            // but it's late in the afternoon and I'm being lazy building out this sample!
            tenancy.AddSingleTenantDatabase(configuration.GetConnectionString("UserContextDb")!,usercontext);
            tenancy.AddSingleTenantDatabase(configuration.GetConnectionString("NotificationSystemDb")!,notificationsystem);
            });
            x.Events.TenancyStyle = TenancyStyle.Conjoined;
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
        .UseLightweightSessions()
        .ApplyAllDatabaseChangesOnStartup()
        .AddAsyncDaemon(Marten.Events.Daemon.Resiliency.DaemonMode.HotCold)
        ;
        return services;
    }
}