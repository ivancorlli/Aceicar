using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Category.Command.AddToSpecialization;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Category;


public sealed record AddToSpecializationRequest(Guid TypeId,Guid SpecializationId);
public static class AddToSpecialization
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] Guid categoryId,
        [FromBody] AddToSpecializationRequest Body,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )
    {
        AddCategoryToSpecializationCommand command = new(categoryId,Body.TypeId,Body.SpecializationId);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command,cancellationToken);
        return ResultConversor.Convert(result);

    }
}