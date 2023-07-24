using CompanyContext.Application.Feature.Type.Dto;
using CompanyContext.Application.Feature.Type.Query.AllTypes;
using Wolverine;

namespace CompanyContext.Api.Controller.Type;


public static class AllTypes
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        CancellationToken cancellationToken,
        IMessageBus Bus
    )
    {
        IList<TypeSummary> result = await Bus.InvokeAsync<IList<TypeSummary>>(new AllTypesQuery());
        return TypedResults.Ok(result);
    }
}