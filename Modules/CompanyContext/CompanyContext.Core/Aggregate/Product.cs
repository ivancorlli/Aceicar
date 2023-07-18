using CompanyContext.Core.Event;
using CompanyContext.Core.Interface;

namespace CompanyContext.Core.Aggregate;

public sealed class Product:IProduct
{

    internal static Product Create(ProductCreated @event)
    {
        return new Product()
        {
            Id = @event.ProductId,
            CategoryId = @event.CategoryId,
        };
    }
    
}