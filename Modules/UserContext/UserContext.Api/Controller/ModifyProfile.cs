using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.User.Command.ProfileModified;
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
        [FromRoute] Guid userId,
        [FromBody] ModifyProfileRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        ModifyProfileCommand command = new(userId, Body.Name, Body.Surname, Body.Gender, DateTime.Parse(Body.Birth));
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}