using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Category.Command.AddToSpecialization;

public sealed record AddCategoryToSpecializationCommand(Guid CategoryId, Guid TypeId, Guid SpecializationId);
public static class AddCategoryToSpecializationHandler
{
    public static async Task<IOperationResult> Handle(
        AddCategoryToSpecializationCommand command,
        IEfWork session,
        CancellationToken cancellationToken
    )
    {
        CompanyContext.Core.Aggregate.Category? category = await session.CategoryRespository.FindById(command.CategoryId);
        if(category == null) return OperationResult.NotFound(new CategoryNotFound());
        category.AddToSpecialization(command.TypeId,command.SpecializationId);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();   
    }
}