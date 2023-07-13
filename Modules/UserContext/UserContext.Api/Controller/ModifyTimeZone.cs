using Common.Basis.Interface;
using Microsoft.AspNetCore.Mvc;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Command.ModifyTimeZone;
using Wolverine;

namespace UserContext.Api.Controller;

public sealed record ModifyTimeZoneRequest(string TimeZone);
public static class ModifyTimeZone
{
    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        [FromRoute] string userId,
        [FromBody] ModifyTimeZoneRequest req,
        IMessageBus bus,
        HttpContext context
    )
    {
        ModifyTimeZoneCommand command = new(userId,req.TimeZone);
        IOperationResult result = await bus.InvokeAsync<IOperationResult>(command);
        return ResultConversor.Convert(result);
    }
    
}