using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Type.Command.CreateSpecialization;

public sealed record CreateSpecializationCommand(Guid TypeId, string Name);
public static class CreateSpecializationHandler
{
    public static async Task<IOperationResult> Handle(
        CreateSpecializationCommand command,
        IEfWork session,
        CancellationToken token
    )   
    {
        Core.Aggregate.Type? type = await session.TypeRepository.FindById(command.TypeId);
        if(type == null) return OperationResult.NotFound(new TypeNotFound());
        Result<Specialization> result = type.AddSpecialization(command.Name);
        if(result.IsFailure) return OperationResult.Invalid(result.Error);
        session.TypeRepository.Update(result.Value);
        await session.SaveChangesAsync(token);
        return OperationResult.Success();
    }
}