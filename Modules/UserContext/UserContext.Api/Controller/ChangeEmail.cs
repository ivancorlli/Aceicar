using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Application.Feature.ApplicationUser.Command.ChangeEmail;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ChangeEmailRequest(
    string Email
);
public static class ChangeEmail
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] ChangeEmailRequest Body,
        IMessageBus Bus
    )
    {
        ChangeEmailCommand command = new("",Body.Email);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return TypedResults.Ok();
    }
}