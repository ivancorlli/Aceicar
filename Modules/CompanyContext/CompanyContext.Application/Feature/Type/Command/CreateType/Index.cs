using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Repository;
using CompanyContext.Core.Service;

namespace CompanyContext.Application.Feature.Type.Command.CreateType;

public sealed record CreateTypeCommand(string Name,string? Icon);
public static class CreateTypeHandler
{
    public static async Task<IOperationResult> Handle(
        CreateTypeCommand command,
        IEfWork session,
        TypeManager manager,
        CancellationToken cancellationToken
    )
    {
        Result<Core.Aggregate.Type> type = await manager.Create(command.Name);
        if(type.IsFailure) return OperationResult.Invalid(type.Error);
        if(command.Icon != null)
        {
            type.Value.ChangeIcon(command.Icon);
        }
        session.TypeRepository.Update(type.Value);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}