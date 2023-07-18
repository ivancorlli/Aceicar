using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Repository;
using CompanyContext.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CompanyContext.Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    private CompanyDbContext Context;
    public CategoryRepository(
        CompanyDbContext context
    )
    {
        Context = context;
    }
    public async void Update(Category Root)
    {
        await Context.Category.AddAsync(Root);
    }

    public async void Update(SubCategory Entity)
    {
        await Context.SubCategory.AddAsync(Entity);
    }

    public async Task<Category?> GetById(Guid CategoryId)
    {
        Category? data = await Context.Category.FindAsync(CategoryId);
        return data;
    }

    public async Task<bool> IsNameUsed(string Name)
    {
        IList<Category> categories = await Context.Category.Where(x=>x.Name.Trim() == Name.Trim().ToLower()).Include(x=>x.SubCategories).ToListAsync();
        if(categories.Count > 0) return true;
        return false;
    }
}