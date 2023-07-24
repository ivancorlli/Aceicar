using System.Security.Claims;
using Common.Basis.Interface;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Query.MyData;
using UserContext.Application.Feature.User.Dto;
using Wolverine;

namespace UserContext.Api.Controller;

public static class MyData
{

    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        Guid? userId,
        HttpContext Context,
        IMessageBus Bus
    )
    {
        Guid UserId = Guid.Empty;
        if(userId != null) {
            UserId = userId.Value;
        }else {
            ClaimsPrincipal claim = Context.User;
            string? idClaim = claim.FindFirstValue("userId");
            if(idClaim != null) UserId = Guid.Parse(idClaim);
        }
        MyDataQuery query = new(UserId);
        IOperationResult<UserSummary> result = await Bus.InvokeAsync<IOperationResult<UserSummary>>(query);
        return ResultConversor.Convert<UserSummary>(result);
    }
    
}