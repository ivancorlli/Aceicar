using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Application.Feature.ApplicationUser.Command.ConfigAccount;
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
        [FromBody] ConfigAccountRequest Body,
        IMessageBus Bus
    ){
        ConfigAccountCommand command = new("",Body.Username,Body.PhoneCountry,Body.PhoneNumber);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return TypedResults.Ok();
    }
}