using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Service.Command.CreateService;
using Wolverine;

namespace CompanyContext.Api.Controller.Service;

public static class CreateService
{
    public sealed record CreateServiceBody(string Name);
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        CreateServiceBody body,
        IMessageBus bus,
        CancellationToken cancellationToken
    )  
    {
        CreateServiceCommand command = new(body.Name);
        IOperationResult result = await bus.InvokeAsync<IOperationResult>(command,cancellationToken);
        return ResultConversor.Convert(result);
    } 
}