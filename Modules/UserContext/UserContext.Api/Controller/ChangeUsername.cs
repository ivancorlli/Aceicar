using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Command.ChangeUsername;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ChangeUsernameRequest(
    string Username
);
public static class ChangeUsername
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] string userId,
        [FromBody] ChangeUsernameRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        string UserId = string.Empty;
        ClaimsPrincipal claim = context.User;
        string? idClaim = claim.FindFirstValue("userId");
        if (idClaim != null) UserId = idClaim;
        if (string.IsNullOrEmpty(UserId)) return TypedResults.BadRequest();
        ChangeUsernameCommand command = new(UserId, Body.Username);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}