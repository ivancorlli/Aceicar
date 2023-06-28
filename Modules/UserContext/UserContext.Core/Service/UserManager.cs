using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Error;
using UserContext.Core.Event.UserEvent;
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

    public async Task<Result<User>> CreateUser(UserCreated @event)
    {
        // search user by email
        User? exists = await _userRepo.FindByEmail(Email.Create(@event.Email));
        if (exists is null)
        {
            // create new user
            User newUser = User.Create(@event);
            // return user created
            return Result.Ok(newUser);
        }
        else return Result.Fail<User>(new UserExists());
    }

    public async Task<Result<User>> ChangeEmail(UserId UserId, Email Email)
    {
        // search user by email
        User? exists = await _userRepo.FindByEmail(Email);
        if (exists != null)
        {
            exists.ChangeEmail(Email);
            return Result.Ok(exists);
        }
        else return Result.Fail<User>(new UserExists());
    }

    public async Task<Result<User>> ChangeUsername(UserId userId, Username username)
    {
        // Search Username already exists 
        User? usernameTaken = await _userRepo.FindByUsername(username);
        if (usernameTaken != null) return Result.Fail<User>(new UserExists());
        // Search user by id
        User? user = await _userRepo.FindById(userId.Value);
        if (user != null)
        {
            user.ChangeUsername(username);
            return Result.Ok(user);
        }
        else return Result.Fail<User>(new UserNotFound());
    }

    public async Task<Result<User>> ChangePhone(UserId userId, Phone phone)
    {
        // Search Phone already exists 
        User? usernameTaken = await _userRepo.FindByPhone(phone);
        if (usernameTaken != null) return Result.Fail<User>(new UserExists());
        // Search user by id
        User? user = await _userRepo.FindById(userId.Value);
        if (user != null)
        {
            user.ChangePhone(phone);
            return Result.Ok(user);
        }
        else return Result.Fail<User>(new UserNotFound());
    }

    public async Task<Result<User>> ConfigAccount(UserId userId, Username username, Phone phone)
    {
        // Search Phone already exists 
        User? usernameTaken = await _userRepo.FindByPhone(phone);
        if (usernameTaken != null) return Result.Fail<User>(new UserExists());
        // Search Username already exists 
        usernameTaken = await _userRepo.FindByUsername(username);
        if (usernameTaken != null) return Result.Fail<User>(new UserExists());
        // Search user by id
        User? user = await _userRepo.FindById(userId.Value);
        if (user != null)
        {
            user.ChangePhone(phone).ChangeUsername(username);
            return Result.Ok(user);
        }
        else return Result.Fail<User>(new UserNotFound());
    }
}