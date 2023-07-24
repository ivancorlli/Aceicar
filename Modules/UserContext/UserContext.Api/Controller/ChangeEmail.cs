using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.User.Command.ChangeEmail;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ChangeEmailRequest(
    string Email
);
public static class ChangeEmail
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] Guid userId,
        [FromBody] ChangeEmailRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        ChangeEmailCommand command = new(userId, Body.Email);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}