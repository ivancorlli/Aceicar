using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Event;

namespace CompanyContext.Core.Repository;

public interface IProductRepository
{
    Task<bool> IsCodeUsed(string Code);
    Task<Product?> FindById(Guid ProductId);
    void Create(Guid ProductId, ProductCreatedForCategory @event);
    void Create(Guid ProductId, ProductCreatedForSubcategory @event);
}