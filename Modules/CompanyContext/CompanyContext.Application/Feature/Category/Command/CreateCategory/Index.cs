using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Repository;
using CompanyContext.Core.Service;

namespace CompanyContext.Application.Feature.Category.Command.CreateCategory;

public sealed record CreateCategoryCommand(string Name);
public static class CreateCategoryHandler
{
    public static async Task<IOperationResult> Handle(
        CreateCategoryCommand command,
        IEfWork session,
        CancellationToken cancellationToken,
        CategoryManager manager 
    )   
    {
        Result<CompanyContext.Core.Aggregate.Category> result = await manager.Create(command.Name);
        if(result.IsFailure) return OperationResult.NotFound(result.Error);
        session.CategoryRespository.CreateAsync(result.Value);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}