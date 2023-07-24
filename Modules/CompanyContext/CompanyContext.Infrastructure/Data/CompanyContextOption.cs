using CompanyContext.Application.ViewModel;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Event;
using CompanyContext.Infrastructure.Projection.CompanyProjector;
using CompanyContext.Infrastructure.Projection.ProductProjector;
using CompanyContext.Infrastructure.Projection.UserAccessProjector;
using Marten;

namespace CompanyContext.Infrastructure.Data;

public static class CompanyContextOption
{
    public static StoreOptions CompanyOption(this StoreOptions option)
    {
        option.Schema.For<CompanyProjection>().Identity(x => x.CompanyId);
        option.Schema.For<UserAccess>().Identity(x => x.AccessId).Index(x => x.CompanyId).Index(x => x.UserId);
        // Register events
        option.Events.AddEventType(typeof(CompanyCreated));
        option.Events.AddEventType(typeof(EmailChanged));
        option.Events.AddEventType(typeof(PhoneChanged));
        option.Events.AddEventType(typeof(AddedToArea));
        option.Events.AddEventType(typeof(CompanyPublished));
        option.Events.AddEventType(typeof(PictureChanged));
        option.Events.AddEventType(typeof(DescriptionChanged));
        option.Events.AddEventType(typeof(LocationChanged));
        option.Events.AddEventType(typeof(AccessCreated));
        // Projections
        option.Projections.LiveStreamAggregation<Company>().Identity(x => x.Id);
        option.Projections.Add<CompanyProjector>(Marten.Events.Projections.ProjectionLifecycle.Inline);
        option.Projections.Add<UserAccessProjector>(Marten.Events.Projections.ProjectionLifecycle.Inline);
        return option;
    }
    public static StoreOptions ProductOption(this StoreOptions option)
    {
        option.Schema.For<Product>().Identity(x => x.Id);
        option.Schema.For<ProductProjection>().Identity(x=>x.ProductId);
        // Register events
        option.Events.AddEventType(typeof(ProductCreatedForCategory));
        option.Events.AddEventType(typeof(ProductCreatedForSubcategory));
        option.Events.AddEventType(typeof(DescriptionAdded));
        option.Events.AddEventType(typeof(AddedToBrand));
        option.Events.AddEventType(typeof(ImageAdded));
        option.Events.AddEventType(typeof(ImageDeleted));
        // Projections
        option.Projections.LiveStreamAggregation<Product>().Identity(x => x.Id);
        option.Projections.Add<ProductProjector>(Marten.Events.Projections.ProjectionLifecycle.Inline);
        return option;
    }
}