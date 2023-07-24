using System.Security.Claims;
using Common.Basis.Interface;
using CompanyContext.Api.utils;
using CompanyContext.Application.Feature.Company.Dto;
using CompanyContext.Application.Feature.Company.Query.AccessById;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Access;

public static class AccessById
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] Guid accessId,
        IMessageBus Bus,
        HttpContext context,
        CancellationToken cancellationToken
    )
    {
        ClaimsPrincipal claim = context.User;
        string? idClaim = claim.FindFirstValue("userId");
        if (idClaim != null)
        {
            Guid UserId = Guid.Parse(idClaim);
            IOperationResult<AccessByIdDto> result = await Bus.InvokeAsync<IOperationResult<AccessByIdDto>>(new AccessByIdQuery(accessId,UserId));
            if (result.ResultType == Common.Basis.Enum.OperationResultType.Ok) return TypedResults.Ok(result.Data);
            return ResultConversor.Convert(result);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }

}