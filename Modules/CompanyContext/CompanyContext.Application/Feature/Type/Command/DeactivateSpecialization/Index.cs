using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Type.Command.DeactivateSpecialization;

public sealed record DeactivateSpecializationCommand(Guid TypeId,Guid SpecializationId);
public static class DeactivateSpecializationHandler
{
    public static async Task<IOperationResult> Handle(
        DeactivateSpecializationCommand command,
        IEfWork session,
        CancellationToken cancellationToken
    )
    {
        Core.Aggregate.Type? type = await session.TypeRepository.FindById(command.TypeId);
        if(type == null ) return OperationResult.NotFound(new TypeNotFound());
        Specialization? specialization = type.Specializations.Where(x=>x.Id == command.SpecializationId).First();
        if(specialization == null) return OperationResult.NotFound(new SpecializationNotFound());
        specialization.Deactivate();
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
    
}