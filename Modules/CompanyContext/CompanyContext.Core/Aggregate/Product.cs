using CompanyContext.Core.Event;
using CompanyContext.Core.Interface;

namespace CompanyContext.Core.Aggregate;

public sealed class Product:IProduct
{
    public static Product Create(ProductCreatedForCategory @event)
    {
        Product newProduct = new()
        {
            Id = @event.ProductId,
            Code = @event.Code.Trim().ToUpper()
        };
        return newProduct;
    }
    
}