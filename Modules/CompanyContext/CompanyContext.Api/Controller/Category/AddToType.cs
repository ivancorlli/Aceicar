using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Category.Command.AddToType;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Category;

public sealed record AddToTypeRequest(Guid TypeId);
public static class AddToType
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] Guid categoryId,
        [FromBody] AddToTypeRequest Body,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )   
    {
        AddCategoryToTypeCommand command = new(categoryId,Body.TypeId);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command,cancellationToken);
        return ResultConversor.Convert(result);
    } 
}