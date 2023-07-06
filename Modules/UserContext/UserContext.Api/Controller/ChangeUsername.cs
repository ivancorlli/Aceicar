using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Application.Feature.ApplicationUser.Command.ChangeUsername;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ChangeUsernameRequest(
    string Username
);
public static class ChangeUsername
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] ChangeUsernameRequest Body,
        IMessageBus Bus
    ){
        ChangeUsernameCommand command = new("",Body.Username);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return TypedResults.Ok();
    }
}