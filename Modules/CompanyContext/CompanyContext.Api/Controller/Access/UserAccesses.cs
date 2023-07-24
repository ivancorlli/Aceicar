using System.Security.Claims;
using CompanyContext.Application.Feature.Company.Dto;
using CompanyContext.Application.Feature.Company.Query.UserAccesses;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace CompanyContext.Api.Controller.Access;

public static class UserAccesses
{
    public static async Task<IResult> Execute(
        [FromRoute] Guid userId,
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
            if (userId != UserId) return TypedResults.Unauthorized();
            var data = await Bus.InvokeAsync<IReadOnlyList<UserAccessSummary>>(new UserAccessesQuery(userId));
            return TypedResults.Ok(data);
        }
        else
        {
            return TypedResults.Unauthorized();
        }
    }
}