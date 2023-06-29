using UserContext.Application.Feature.ApplicationUser.Query.AllUsers;
using UserContext.Core.Aggregate;
using Wolverine;

namespace UserContext.Api.Controller;

public static class Allusers
{
    public static async Task<IResult> Execute(
        HttpContext Context,
        IMessageBus Bus
    )
    {
        AllUsersCommand @command = new();
        IEnumerable<User> result = await Bus.InvokeAsync<IEnumerable<User>>(command);
        if(result.Count()>0) return TypedResults.Ok(new {users=result});
            else return TypedResults.NoContent();

    }
    
}