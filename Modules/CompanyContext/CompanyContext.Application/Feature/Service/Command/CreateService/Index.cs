using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Repository;
using CompanyContext.Core.Service;

namespace CompanyContext.Application.Feature.Service.Command.CreateService;

public sealed record CreateServiceCommand(string Name);
public static class CreateServiceHandler
{
    public static async Task<IOperationResult> Handle(
        CreateServiceCommand command,
        IEfWork session,
        ServiceManager manager,
        CancellationToken cancellationToken
    )
    {
        Result<CompanyContext.Core.Aggregate.Service> result = await manager.Create(command.Name);
        if(result.IsFailure) return OperationResult.Invalid(result.Error);
        session.ServiceRepository.Update(result.Value);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }

}