using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Service.Command.AddToSpecialization;

public sealed record AddServiceToSpecializationCommand(Guid ServiceId,Guid TypeId,Guid SpecializationId);
public static class AddServiceToSpecializationHandler
{
    public static async Task<IOperationResult> Handle(
        AddServiceToSpecializationCommand command,
        IEfWork session,
        CancellationToken cancellationToken
    )
    {
        CompanyContext.Core.Aggregate.Service? service = await session.ServiceRepository.FindById(command.ServiceId);
        if(service == null) return OperationResult.NotFound(new ServiceNotFound());
        service.AddToSpecialization(command.TypeId,command.SpecializationId);
        session.ServiceRepository.Update(service);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}