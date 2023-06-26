using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.CreateUser;

public record CreateUserCommand(string Email);


public class CreateUserHandler
{


    public OperationResult<User> Handle(CreateUserCommand command, IUserRepository _repo, UserManager _manager)
    {
        Email Email = new(command.Email);
        Result<User> newUser = _manager.CreateUser(Email);
        if (newUser.IsSuccess)
        {
            User user = newUser.Value;
            _repo.Create(user);
            return new SuccessResult<User>(user);
        }
        else return new InvalidResult<User>(newUser.Error.Message);

    }

}