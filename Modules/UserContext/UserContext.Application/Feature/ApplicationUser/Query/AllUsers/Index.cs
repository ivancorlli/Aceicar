using UserContext.Core.Aggregate;
using UserContext.Core.Repository;

namespace UserContext.Application.Feature.ApplicationUser.Query.AllUsers;

public sealed record AllUsersCommand();

public sealed class AllUsersHandler
{

    public static void Handle(
        AllUsersCommand command,
        IUoW _session
    ) 
    {
        //IEnumerable<User> allUsers = await _session.UserRepository.FindAll();
        //return allUsers;
    }

}