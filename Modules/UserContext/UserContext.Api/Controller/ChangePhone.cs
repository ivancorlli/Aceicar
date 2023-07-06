using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Application.Feature.ApplicationUser.Command.ChangePhone;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ChangePhoneRequest(
    string PhoneCountry,
    string PhoneNumber
);
public static class ChangePhone
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] ChangePhoneRequest Body,
        IMessageBus Bus
    )
    {
        ChangePhoneCommand command = new("",Body.PhoneCountry,Body.PhoneNumber);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return TypedResults.Ok();
    }
}