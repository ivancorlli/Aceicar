using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;
namespace CompanyContext.Core.Service;

public class CategoryManager
{
    private readonly ICategoryRepository category;
    public CategoryManager(ICategoryRepository repo)
    {
        category = repo;
    }

    public async Task<Result<Category>> Create(string name)
    {
        bool isUsed = await category.IsNameUsed(name);
        if(isUsed) return Result.Fail<Category>(new CategoryExists());
        Category newCategory = new Category(name);
        return Result.Ok(newCategory);    
    }     
}