using CompanyContext.Application.Feature.Type.Dto;
using CompanyContext.Application.Feature.Type.Query.SpecializationsForType;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Type;

public static class SpecializationsByType
{
    public static async Task<IResult> Execute(
        [FromRoute] Guid typeId,
        IMessageBus Bus,
        CancellationToken cancellationToken
    )
    {
        IList<SpecializationSummary> result = await Bus.InvokeAsync<IList<SpecializationSummary>>(new SpecializationByTypeQuery(typeId));
        return TypedResults.Ok(result);
    }
}