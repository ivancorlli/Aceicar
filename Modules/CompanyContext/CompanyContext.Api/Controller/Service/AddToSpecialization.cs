using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Service.Command.AddToSpecialization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Service;

public static class AddToSpecialization
{
    public sealed record AddToSpecializationBody(Guid TypeId,Guid SpecializationId);
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        Guid serviceId,
        [FromBody] AddToSpecializationBody body,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )
    {
        AddServiceToSpecializationCommand command = new(serviceId,body.TypeId,body.SpecializationId);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command,cancellationToken);
        return ResultConversor.Convert(result);
    }
}