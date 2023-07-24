using System.Security.Claims;
using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.User.Command.LocationModified;
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
        [FromRoute] Guid userId,
        [FromBody] ModifyLocationRequest Body,
        IMessageBus Bus,
        HttpContext context
    )
    {
        ModifyLocationCommand command = new(userId, Body.Country, Body.City, Body.State, Body.PostalCode);
        IOperationResult result = await Bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
}