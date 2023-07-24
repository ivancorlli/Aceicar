using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Event;
using CompanyContext.Core.Repository;
using CompanyContext.Core.Service;

namespace CompanyContext.Application.Feature.Product.Command.CreateProduct;

public sealed record CreateProductCommand(
    string Code,
    string Name,
    Guid CategoryId,
    Guid? SubCategoryId = null,
    Guid? CompanyId = null
    );
public static class CreateProductHandler
{
    public static async Task<IOperationResult> Handle(
        CreateProductCommand command,
        IUoW session,
        ProductManager manager,
        CancellationToken cancellationToken
    )
    {
        if (command.SubCategoryId == null)
        {
            Result<ProductCreatedForCategory> result = await manager.Create(command.Code, command.Name, command.CategoryId, command.CompanyId);
            if (result.IsFailure) return OperationResult.Invalid(result.Error);
            session.ProductRepository.Create(result.Value.ProductId, result.Value);
        }
        else
        {
            Result<ProductCreatedForSubcategory> result = await manager.Create(command.Code, command.Name, command.CategoryId, command.SubCategoryId.Value, command.CompanyId);
            session.ProductRepository.Create(result.Value.ProductId, result.Value);
            if (result.IsFailure) return OperationResult.Invalid(result.Error);
        }
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}