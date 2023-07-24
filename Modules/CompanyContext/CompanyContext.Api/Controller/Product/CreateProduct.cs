using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Product.Command.CreateProduct;
using Wolverine;

namespace CompanyContext.Api.Controller.Product;

public sealed record CreateProdcutBody(
    string Code,
    string Name,
    Guid CategoryId,
    Guid? SubCategoryId
);
public static class CreateProduct
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        CreateProdcutBody body,
        IMessageBus bus,
        CancellationToken cancellationToken
    )
    {
        CreateProductCommand command = new(body.Code,body.Name,body.CategoryId,body.SubCategoryId);
        IOperationResult result = await bus.InvokeAsync<IOperationResult>(command,cancellationToken);
        return ResultConversor.Convert(result);
    }
}