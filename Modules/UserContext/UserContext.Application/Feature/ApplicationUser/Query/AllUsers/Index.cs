using UserContext.Core.Aggregate;
using UserContext.Core.Repository;

namespace UserContext.Application.Feature.ApplicationUser.Query.AllUsers;

public sealed record AllUsersCommand();

public sealed class AllUsersHandler
{

    public static async Task<IEnumerable<User>> Handle(
        AllUsersCommand command,
        IUoW _session
    ) 
    {
        IEnumerable<User> allUsers = await _session.UserRepository.FindAll();
        return allUsers;
    }

}