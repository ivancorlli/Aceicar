using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Type.Command.CreateSpecialization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Type;

public static class CreateSpecialization
{
    public sealed record CreateSpecializationBody(string Name);
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] Guid typeId,
        [FromBody] CreateSpecializationBody Body,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )
    {
        CreateSpecializationCommand command = new(typeId,Body.Name);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command,cancellationToken);
        return ResultConversor.Convert(result);
    }
}