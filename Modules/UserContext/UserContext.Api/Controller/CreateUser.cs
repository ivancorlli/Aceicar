using Common.Basis.Enum;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Application.Feature.ApplicationUser.Command.CreateUser;
using Wolverine;

namespace UserContext.Api.Controller;


public sealed record CreateUserRequest(string Email);


public static class CreateUser
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] CreateUserRequest NewEmail,
        HttpContext Context,
        IMessageBus Bus
    )
    {
        CreateUserCommand @command = new(NewEmail.Email);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        if(result.ResultType == OperationResultType.Ok) return TypedResults.Ok(new {Message="User created succesfully"});
        return Results.NoContent();
    }
}