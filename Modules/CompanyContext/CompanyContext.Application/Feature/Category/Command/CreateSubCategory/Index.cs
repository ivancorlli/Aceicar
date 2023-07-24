using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Category.Command.CreateSubCategory;

public sealed record CreateSubCategoryCommand(Guid CategoryId,string Name);
public static class CreateSubCategoryHandler
{
    public static async Task<IOperationResult> Handle(
        CreateSubCategoryCommand command,
        IEfWork session,
        CancellationToken cancellationToken   
    )   
    {
        CompanyContext.Core.Aggregate.Category? category = await session.CategoryRespository.FindById(command.CategoryId);
        if(category == null) return OperationResult.NotFound(new CategoryNotFound());
        Result<SubCategory> subCategory = category.CreateSubCategory(command.Name);
        if(subCategory.IsFailure) return OperationResult.Invalid(subCategory.Error);
        session.CategoryRespository.Update(subCategory.Value);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}