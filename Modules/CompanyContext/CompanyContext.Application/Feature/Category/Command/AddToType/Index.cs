using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Category.Command.AddToType;

public sealed record AddCategoryToTypeCommand(Guid CategoryId,Guid TypeId);
public static class AddCategoryToTypeHandler 
{
    public static async Task<IOperationResult> Handle(
        AddCategoryToTypeCommand command,
        IEfWork session,
        CancellationToken cancellationToken
    )
    {
        CompanyContext.Core.Aggregate.Category? category = await session.CategoryRespository.FindById(command.CategoryId);
        if(category == null) return OperationResult.NotFound(new CategoryNotFound());
        category.AddToType(command.TypeId);
        session.CategoryRespository.Update(category);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}