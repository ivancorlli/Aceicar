using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Type.Command.DeactivateType;

public sealed record DeactivateTypeCommand(Guid TypeId);
public static class DeactivateTypeHandler
{
    public static async Task<IOperationResult> Handle(
        DeactivateTypeCommand command,
        IEfWork session,
        CancellationToken token
    )
    {
        CompanyType? companyType = await session.CompanyTypeRepository.GetById(command.TypeId);
        if(companyType == null) return OperationResult.NotFound(new CompanyTypeNotFound());
        companyType.Deactivate();
        await session.SaveChangesAsync(token);
        return OperationResult.Success();
    }

}