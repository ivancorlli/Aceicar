using Marten;
using Marten.Events.Projections;
using Marten.Schema;
using UserContext.Core.Aggregate;

namespace UserContext.Infrastructure.Data;

public static class UserConfiguration
{

    public static StoreOptions ConfigureUser(this StoreOptions options)
    {
        options.Schema.For<User>().Identity(x=>x.Id);
        options.Schema.For<User>().Index(x=>x.Id).UniqueIndex(x=>x.Id);
        return options;
    } 

}