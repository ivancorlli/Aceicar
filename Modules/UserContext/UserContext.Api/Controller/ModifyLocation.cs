using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Command.LocationModified;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ModifyLocationRequest(
    string Country,
    string City,
    string State,
    string PostalCode
);
public static class ModifyLocation
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] string userId,
        [FromBody] ModifyLocationRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        string UserId = string.Empty;
        ClaimsPrincipal claim = context.User;
        string? idClaim = claim.FindFirstValue("userId");
        if (idClaim != null) UserId = idClaim;
        if (string.IsNullOrEmpty(UserId)) return TypedResults.BadRequest();
        ModifyLocationCommand command = new(UserId, Body.Country, Body.City, Body.State, Body.PostalCode);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}