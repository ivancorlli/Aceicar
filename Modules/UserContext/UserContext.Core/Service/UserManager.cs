using Common.Basis.Error;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Enumerable;
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
        Email email = Email.Create(@event.Email);
        bool exists = await _userRepo.IsEmailUsed(email);
        if (exists) return Result.Fail<User>(new EmailAlreadyUsed(email));
        // create new user
        User newUser = User.Create(@event);
        // return user created
        return Result.Ok(newUser);
    }

    public async Task<Result<User>> ChangeEmail(UserId UserId, Email Email)
    {
        // search user by email
        bool exists = await _userRepo.IsEmailUsed(Email);
        if (exists) return Result.Fail<User>(new EmailAlreadyUsed(Email));

        User? user = await _userRepo.FindById(UserId.Value);
        if (user == null) return Result.Fail<User>(new UserNotFound());
        user.ChangeEmail(Email);
        return Result.Ok(user);

    }

    public async Task<Result<User>> ChangeUsername(UserId userId, Username username)
    {
        // Search Username already exists 
        bool? usernameTaken = await _userRepo.IsUsernameUsed(username);
        if (usernameTaken != null) return Result.Fail<User>(new UsernameAlreadyUsed(username));
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
        bool? usernameTaken = await _userRepo.IsPhoneUsed(phone);
        if (usernameTaken != null) return Result.Fail<User>(new PhoneAlreadyUsed(phone));
        // Search user by id
        User? user = await _userRepo.FindById(userId.Value);
        if (user != null)
        {
            user.ChangePhone(phone);
            return Result.Ok(user);
        }
        else return Result.Fail<User>(new UserNotFound());
    }

    public async Task<Result<User>> ConfigAccount(UserId userId, Username? username, Phone? phone)
    {
        // Search Phone already exists 
        if (phone != null)
        {
            bool phoneTaken = await _userRepo.IsPhoneUsed(phone);
            if (phoneTaken) return Result.Fail<User>(new PhoneAlreadyUsed(phone));
        }
        // Search Username already exists 
        if (username != null)
        {
            bool usernameTaken = await _userRepo.IsUsernameUsed(username);
            if (usernameTaken) return Result.Fail<User>(new UsernameAlreadyUsed(username));
        }
        // Search user by id
        if (username == null && phone == null) return Result.Fail<User>(new DomainError(ErrorTypes.TypeBuilder(nameof(UserContext),nameof(UserManager)),"Must send username or phone"));
            User? user = await _userRepo.FindById(userId.Value);
            if (user != null)
            {
                if(username != null) user.ChangeUsername(username);
                if(phone != null) user.ChangePhone(phone);
                return Result.Ok(user);
            }
            else return Result.Fail<User>(new UserNotFound());
    }
}