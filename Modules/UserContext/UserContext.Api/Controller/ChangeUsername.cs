using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.User.Command.ChangeUsername;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ChangeUsernameRequest(
    string Username
);
public static class ChangeUsername
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] Guid userId,
        [FromBody] ChangeUsernameRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        ChangeUsernameCommand command = new(userId, Body.Username);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}