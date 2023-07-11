using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Command.ConfigAccount;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ConfigAccountRequest(
    string Username,
    string PhoneCountry,
    string PhoneNumber
);
public static class ConfigAccount
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] string userId,
        [FromBody] ConfigAccountRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        string UserId = string.Empty;
        ClaimsPrincipal claim = context.User;
        string? idClaim = claim.FindFirstValue("userId");
        if (idClaim != null) UserId = idClaim;
        if(string.IsNullOrEmpty(UserId)) return TypedResults.BadRequest();
        ConfigAccountCommand command = new(UserId, Body.Username, Body.PhoneCountry, Body.PhoneNumber);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}