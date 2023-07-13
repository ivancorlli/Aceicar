using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
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
        CompanyType? type = await session.CompanyTypeRepository.GetById(command.TypeId);
        if(type == null) return OperationResult.NotFound(new CompanyTypeNotFound());
        Result result = type.AddSpecialization(command.Name);
        if(result.IsFailure) return OperationResult.Invalid(result.Error);
        await session.SaveChangesAsync(token);
        return OperationResult.Success();
    }
}