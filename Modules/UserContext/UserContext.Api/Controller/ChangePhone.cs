using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Command.ChangePhone;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ChangePhoneRequest(
    string PhoneCountry,
    string PhoneNumber
);
public static class ChangePhone
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] string userId,
        [FromBody] ChangePhoneRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        string UserId = string.Empty;
        ClaimsPrincipal claim = context.User;
        string? idClaim = claim.FindFirstValue("userId");
        if (idClaim != null) UserId = idClaim;
        if (string.IsNullOrEmpty(UserId)) return TypedResults.BadRequest();
        ChangePhoneCommand command = new(UserId, Body.PhoneCountry, Body.PhoneNumber);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}