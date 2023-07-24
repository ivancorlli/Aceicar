using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Dto;
using UserContext.Application.Feature.User.Command.CreateUser;
using Wolverine;

namespace UserContext.Api.Controller;


public sealed record CreateUserRequest(
    string Email,
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
        CreateUserCommand @command = new(req.Email,req.TimeZone);
        IOperationResult<CreateUserDto> result = await Bus.InvokeAsync<IOperationResult<CreateUserDto>>(command);
        if(result.ResultType == Common.Basis.Enum.OperationResultType.Created) return Results.Ok(result.Data);
        return ResultConversor.Convert(result);
    }
}