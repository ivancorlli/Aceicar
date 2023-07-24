using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Error;
using CompanyContext.Core.Event;
using CompanyContext.Core.Repository;

namespace CompanyContext.Core.Service;

public sealed class ProductManager
{
    private readonly IProductRepository _product;
    public ProductManager(
        IProductRepository product
    )
    {
        _product = product;
    }

    public async Task<Result<ProductCreatedForCategory>> Create(string code, string name, Guid categoryId, Guid? companyId = null)
    {
        bool isUsed = await _product.IsCodeUsed(code.Trim().ToUpper());
        if (isUsed) return Result.Fail<ProductCreatedForCategory>(new ProductExists());
        return Result.Ok(new ProductCreatedForCategory(Guid.NewGuid(), code.Trim().ToUpper(), name, categoryId, companyId));
    }

    public async Task<Result<ProductCreatedForSubcategory>> Create(string code, string name, Guid categoryId, Guid SubCategoryId, Guid? companyId = null)
    {
        bool isUsed = await _product.IsCodeUsed(code.Trim().ToUpper());
        if (isUsed) return Result.Fail<ProductCreatedForSubcategory>(new ProductExists());
        return Result.Ok(new ProductCreatedForSubcategory(Guid.NewGuid(), code.Trim().ToUpper(), name, categoryId, SubCategoryId, companyId));

    }
    public Result<ImageAdded> AddImage(string image,Product product)
    {
        if(product.Images.Count >= 3) return Result.Fail<ImageAdded>(new ImageCountLimit());
        return Result.Ok(new ImageAdded(product.Id,image));
    }
}