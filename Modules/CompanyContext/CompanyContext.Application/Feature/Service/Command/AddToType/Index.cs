using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Service.Command.AddToType;

public sealed record AddServiceToTypeCommand(Guid ServiceId, Guid TypeId);
public static class AddServiceToTypeHandler
{
    public static async Task<IOperationResult> Handle(
        AddServiceToTypeCommand command,
        IEfWork session,
        CancellationToken cancellationToken
    )
    {
        CompanyContext.Core.Aggregate.Service? service = await session.ServiceRepository.GetById(command.ServiceId);
        if(service == null) return OperationResult.NotFound(new ServiceNotFound());
        service.AddToType(command.TypeId);
        session.ServiceRepository.Update(service);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}