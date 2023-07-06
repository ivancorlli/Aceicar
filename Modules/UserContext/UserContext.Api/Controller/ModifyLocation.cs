using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Application.Feature.ApplicationUser.Command.LocationModified;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ModifyLocationRequest(
    string Country,
    string City,
    string State,
    string PostalCode
);
public static class ModifyLocation
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromBody] ModifyLocationRequest Body,
        IMessageBus Bus
    )
    {
        ModifyLocationCommand command = new("",Body.Country,Body.City,Body.State,Body.PostalCode);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return TypedResults.Ok();
    }
}