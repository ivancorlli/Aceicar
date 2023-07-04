using UserContext.Application.ViewModel;
using UserContext.Core.ValueObject;

namespace UserContext.Application.Service;

public interface IUserAccountService
{
    Task<UserAccount?> FindById(Guid Id);
    Task<UserAccount?> FindByEmail(Email Email); 
}