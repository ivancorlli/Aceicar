using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Role.Command.CreateRole;
using Wolverine;

namespace CompanyContext.Api.Controller.Role;

public static class CreateRole
{
    public sealed record CreateRoleBody(string Name);
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        CreateRoleBody Body,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )   
    {
        CreateRoleCommand command = new(Body.Name);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command,cancellationToken);
        return ResultConversor.Convert(result);
    }
}