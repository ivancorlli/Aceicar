using Common.Basis.Enum;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Application.Feature.ApplicationUser.Command.CreateUserWithProvider;
using UserContext.Core.ValueObject;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record CreateUserProviderRequest(
    string Email,
    string? Username,
    string? Phone,
    string? Name,
    string? Surname
    );
public static class CreateUserProvider
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] CreateUserProviderRequest Body,
        IMessageBus Bus
    )
    {
        CreateUserWithProviderCommand command = new(Body.Email);
        IOperationResult<UserId> result = await Bus.InvokeAsync<IOperationResult<UserId>>(command);
        if(result.ResultType == OperationResultType.Invalid) return TypedResults.BadRequest(new {Message=result.Errors.First()});
        return TypedResults.Ok(new {UserId=result.Data.Value});
    }
}