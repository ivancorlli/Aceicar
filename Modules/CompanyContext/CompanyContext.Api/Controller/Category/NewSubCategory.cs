using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Category.Command.CreateSubCategory;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Category;


public static class NewSubCategory
{
    public sealed record NewSubCategoryRequest(string Name);
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] NewSubCategoryRequest Body,
        [FromRoute] Guid CategoryId,
        IMessageBus Bus
    )
    {
        CreateSubCategoryCommand command = new(CategoryId, Body.Name);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}