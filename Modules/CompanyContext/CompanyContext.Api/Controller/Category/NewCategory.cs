using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Category.Command.CreateCategory;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Category;


public sealed record NewCategoryRequest(string Name);
public static class NewCategory
{
    public  static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] NewCategoryRequest Body,
        IMessageBus Bus
    )
    {
        CreateCategoryCommand command = new(Body.Name);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
    
}