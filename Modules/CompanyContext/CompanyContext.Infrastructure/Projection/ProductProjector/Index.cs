using CompanyContext.Application.ViewModel;
using CompanyContext.Core.Event;
using Marten.Events.Aggregation;

namespace CompanyContext.Infrastructure.Projection.ProductProjector;

public sealed class ProductProjector : SingleStreamProjection<ProductProjection>
{
    public static ProductProjection Create(ProductCreatedForCategory @event)
    {
        return new ProductProjection()
        {
            ProductId = @event.ProductId,
            Name = @event.Name,
            Code = @event.Code.Trim().ToUpper(),
            CategoryId = @event.CategoryId,
            CompanyId = @event.CompanyId
        };
    }
    public static ProductProjection Create(ProductCreatedForSubcategory @event)
    {
        return new ProductProjection()
        {
            ProductId = @event.ProductId,
            Name = @event.Name,
            Code = @event.Code.Trim().ToUpper(),
            CategoryId = @event.CategoryId,
            CompanyId = @event.CompanyId,
            SubCategoryId = @event.SubCategoryId
        };
    }
    public void Apply(DescriptionAdded @event, ProductProjection product)
    {
        product.Description = @event.Description;
    }
    public void Apply(AddedToBrand @event, ProductProjection product)
    {
        product.BrandId = @event.BrandId;
    }
    public void Apply(ImageAdded @event, ProductProjection product)
    {
        product.Images.Add(@event.Image);
    }
    public void Apply(ImageDeleted @event, ProductProjection product)
    {
        IList<string> exists = product.Images.Where(x => x == @event.Image).ToList();
        if (exists.Count > 0)
        {
            product.Images.Remove(exists.First());
        }
    }
}