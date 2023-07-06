using Common.Basis.Enum;
using Common.Basis.Interface;
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
        Console.WriteLine(Context);
        string UserId = string.Empty;
        if(userId != null) UserId = userId;
        MyDataCommand command = new(UserId);
        IOperationResult<UserAccount> result = await Bus.InvokeAsync<IOperationResult<UserAccount>>(command);
        if(result.ResultType == OperationResultType.NotFound) return TypedResults.NotFound<UserAccount>(result.Data);
        if(result.ResultType == OperationResultType.Invalid) return TypedResults.BadRequest(result.Errors.First());
        return TypedResults.Ok(new {user = result.Data});
    }
    
}