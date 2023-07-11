using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Command.ChangeEmail;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ChangeEmailRequest(
    string Email
);
public static class ChangeEmail
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] string userId,
        [FromBody] ChangeEmailRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        string UserId = string.Empty;
        ClaimsPrincipal claim = context.User;
        string? idClaim = claim.FindFirstValue("userId");
        if (idClaim != null) UserId = idClaim;
        if (string.IsNullOrEmpty(UserId)) return TypedResults.BadRequest();
        ChangeEmailCommand command = new(UserId, Body.Email);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}