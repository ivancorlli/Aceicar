using Common.Basis.Interface;
using Common.Basis.Utils;
using UserContext.Core.Aggregate;
using UserContext.Core.Repository;
using UserContext.Core.Service;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Feature.ApplicationUser.Command.ConfigAccount;

public record ConfigAccountCommand(
    string UserId,
    string Username,
    string PhoneCountry,
    string PhoneNumber
    );

public class ConfigAccountHandler
{
    public OperationResult<User> Handle(ConfigAccountCommand command,IUserRepository _repo, UserManager _manager)
    {
        UserId UserId = new(command.UserId);
        Username Username = Username.Create(command.Username);
        Phone Phone = Phone.Create(command.PhoneCountry,command.PhoneNumber);
        Result<User> result = _manager.ConfigAccount(UserId,Username,Phone); 
        if(result.IsSuccess) 
        {
            User user = result.Value;
            _repo.Save(user);
            return new SuccessResult<User>(user);
        }else return new InvalidResult<User>(result.Error.Message);
    }
}