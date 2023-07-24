using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.User.Command.ConfigAccount;
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
        [FromRoute] Guid userId,
        [FromBody] ConfigAccountRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        ConfigAccountCommand command = new(userId, Body.Username, Body.PhoneCountry, Body.PhoneNumber);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}