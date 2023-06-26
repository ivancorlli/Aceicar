using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Error;
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

    public Result<User> CreateUser(Email email)
    {
        // search user by email
        User? exists = _userRepo.FindByEmail(email);
        if (exists is null)
        {
            // create new user
            User newUser = new User(email);
            // return user created
            return Result.Ok(newUser);
        }
        else return Result.Fail<User>(new UserExists());

    }

    public Result<User> ChangeUsername(UserId userId, Username username)
    {
        // Search Username already exists 
        User? usernameTaken = _userRepo.FindByUsername(username);
        if (usernameTaken != null) return Result.Fail<User>(new UserExists());
        // Search user by id
        User? user = _userRepo.FindById(userId.Value);
        if (user != null)
        {
            user.ChangeUsername(username);
            return Result.Ok(user);
        }
        else return Result.Fail<User>(new UserNotFound());
    }

    public Result<User> ChangePhone(UserId userId, Phone phone)
    {
        // Search Phone already exists 
        User? usernameTaken = _userRepo.FindByPhone(phone);
        if (usernameTaken != null) return Result.Fail<User>(new UserExists());
        // Search user by id
        User? user = _userRepo.FindById(userId.Value);
        if (user != null)
        {
            user.ChangePhone(phone);
            return Result.Ok(user);
        }
        else return Result.Fail<User>(new UserNotFound());
    }

    public Result<User> ConfigAccount(UserId userId,Username username, Phone phone)
    {
        // Search Phone already exists 
        User? usernameTaken = _userRepo.FindByPhone(phone);
        if (usernameTaken != null) return Result.Fail<User>(new UserExists());
        // Search Username already exists 
        usernameTaken = _userRepo.FindByUsername(username);
        if (usernameTaken != null) return Result.Fail<User>(new UserExists());
        // Search user by id
        User? user = _userRepo.FindById(userId.Value);
        if (user != null)
        {
            user.ChangePhone(phone).ChangeUsername(username);
            return Result.Ok(user);
        }
        else return Result.Fail<User>(new UserNotFound());
    }
}