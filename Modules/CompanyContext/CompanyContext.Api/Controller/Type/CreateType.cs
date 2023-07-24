using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Type.Command.CreateType;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Type;


public static class CreateType
{
    public sealed record CreateTypeBody(string Name, string? Icon);
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] CreateTypeBody Body,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )
    {
        CreateTypeCommand command = new(Body.Name,Body.Icon);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command,cancellationToken);
        return ResultConversor.Convert(result);
    }
}