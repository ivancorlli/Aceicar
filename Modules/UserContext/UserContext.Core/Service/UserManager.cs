using UserContext.Core.Aggregate;
using UserContext.Core.Repository;
using UserContext.Core.ValueObject;

namespace UserContext.Core.Service;

public class UserManager
{
    private IUserRepository _userRepo;
    public UserManager(IUserRepository repo)
    {
        _userRepo = repo;
    }

    public User? Create(Email email)
    {
        // search user by email
        User? exists = _userRepo.FindByEmail(email);
        if (exists is not null)
        {
            // create new user
            User newUser = new User(email);
            // return user created
            return newUser;
        }
        else return null;

    }

}