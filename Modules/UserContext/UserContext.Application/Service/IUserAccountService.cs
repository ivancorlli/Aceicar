using Common.Basis.ValueObject;
using UserContext.Application.ViewModel;

namespace UserContext.Application.Service;

public interface IUserAccountService
{
    Task<UserAccount?> FindById(Guid Id);
    Task<UserAccount?> FindByEmail(Email Email); 
}