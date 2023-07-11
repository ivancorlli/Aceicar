using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Command.ProfileModified;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ModifyProfileRequest(
    string Name,
    string Surname,
    string Gender,
    string Birth

);
public static class ModifyProfile
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] string userId,
        [FromBody] ModifyProfileRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        string UserId = string.Empty;
        ClaimsPrincipal claim = context.User;
        string? idClaim = claim.FindFirstValue("userId");
        if (idClaim != null) UserId = idClaim;
        if (string.IsNullOrEmpty(UserId)) return TypedResults.BadRequest();
        ModifyProfileCommand command = new(UserId, Body.Name, Body.Surname, Body.Gender, DateTime.Parse(Body.Birth));
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}