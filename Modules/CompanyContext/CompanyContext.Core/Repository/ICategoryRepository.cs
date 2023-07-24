using CompanyContext.Core.Aggregate;

namespace CompanyContext.Core.Repository;

using CompanyContext.Core.Entity;

public interface ICategoryRepository 
{
    Task<Category?> FindById(Guid CategoryId);
    Task<bool> IsNameUsed(string Name);
    void CreateAsync(Category Root);
    void Update(Category Root);
    void Update(SubCategory Entity);    
}