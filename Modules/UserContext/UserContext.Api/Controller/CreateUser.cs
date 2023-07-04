using Common.Basis.Enum;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Application.Feature.ApplicationUser.Command.CreateUser;
using Wolverine;

namespace UserContext.Api.Controller;


public sealed record CreateUserRequest(
    string Email,
    string TimeZoneCountry,
    string TimeZone
    );
public static class CreateUser
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] CreateUserRequest req,
        HttpContext Context,
        IMessageBus Bus
    )
    {
        CreateUserCommand @command = new(req.Email,req.TimeZoneCountry,req.TimeZone);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        if(result.ResultType == OperationResultType.Ok) return TypedResults.Ok(new {Message="User created succesfully"});
        if(result.ResultType == OperationResultType.Invalid) return TypedResults.BadRequest(new {Message = result.Errors.First()});
        return Results.NoContent();
    }
}