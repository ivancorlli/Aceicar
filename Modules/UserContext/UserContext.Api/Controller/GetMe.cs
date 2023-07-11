using System.Security.Claims;
using Common.Basis.Enum;
using Common.Basis.Interface;
using UserContext.Api.utils;
using UserContext.Application.Feature.ApplicationUser.Query.MyData;
using UserContext.Application.ViewModel;
using Wolverine;

namespace UserContext.Api.Controller;

public static class GetMe
{

    public static async Task<Microsoft.AspNetCore.Http.IResult> Execute(
        string? userId,
        HttpContext Context,
        IMessageBus Bus
    )
    {
        string UserId = string.Empty;
        if(userId != null) {
            UserId = userId;
        }else {
            ClaimsPrincipal claim = Context.User;
            string? idClaim = claim.FindFirstValue("userId");
            if(idClaim != null) UserId = idClaim;
        }
        MyDataCommand command = new(UserId);
        IOperationResult<UserAccount> result = await Bus.InvokeAsync<IOperationResult<UserAccount>>(command);
        return ResultConversor.Convert<UserAccount>(result);
    }
    
}