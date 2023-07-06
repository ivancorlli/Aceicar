using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
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
        [FromBody] ModifyProfileRequest Body,
        IMessageBus Bus
    )
    {
        ModifyProfileCommand command = new("",Body.Name,Body.Surname,Body.Gender,DateTime.Parse(Body.Birth));
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return TypedResults.Ok();
    }
}