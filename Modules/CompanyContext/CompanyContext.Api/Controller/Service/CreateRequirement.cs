using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Service.Command.CreateRequirement;
using Wolverine;

namespace CompanyContext.Api.Controller.Service;

public static class CreateRequirement
{
    public sealed record CreateRequirementBody(Guid CategoryId, Guid? SubCategoryId);
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        Guid serviceId,
        CreateRequirementBody body,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )
    {
        CreateRequirementCommand command = new(serviceId, body.CategoryId, body.SubCategoryId);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command, cancellationToken);
        return ResultConversor.Convert(result);
    }
}