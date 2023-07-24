using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Repository;
using CompanyContext.Core.Service;

namespace CompanyContext.Application.Feature.Role.Command.CreateRole;

public sealed record CreateRoleCommand(string Name);
public static class CreateRoleHandler
{
 public static async Task<IOperationResult> Handle(
    CreateRoleCommand command,
    RoleManager manager,
    IEfWork session,
    CancellationToken cancellationToken
 )
 {
    Result<CompanyContext.Core.Aggregate.Role> role = await manager.Create(command.Name);
    if(role.IsFailure) return OperationResult.Invalid(role.Error);
    session.RoleRepository.Update(role.Value);
    await session.SaveChangesAsync(cancellationToken);
    return OperationResult.Success();
 }

}