using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Service.Command.AddToType;
using Wolverine;

namespace CompanyContext.Api.Controller.Service;


public static class AddToType
{
    public sealed record AddToTypeBody(Guid TypeId);
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        Guid serviceId,
        AddToTypeBody body,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )
    {
        AddServiceToTypeCommand command = new(serviceId, body.TypeId);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command, cancellationToken);
        return ResultConversor.Convert(result);

    }
}