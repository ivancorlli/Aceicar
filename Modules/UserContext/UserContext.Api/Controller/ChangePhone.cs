using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.User.Command.ChangePhone;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ChangePhoneRequest(
    string PhoneCountry,
    string PhoneNumber
);
public static class ChangePhone
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] Guid userId,
        [FromBody] ChangePhoneRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        ChangePhoneCommand command = new(userId, Body.PhoneCountry, Body.PhoneNumber);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}