using Marten;
using UserContext.Application.Service;
using UserContext.Application.ViewModel;
using UserContext.Core.ValueObject;

namespace UserContext.Infrastructure.Service;

public class UserAccountService : IUserAccountService
{
    private readonly IQuerySession _session;
    public UserAccountService(IQuerySession session)
    {
        _session = session;
    }

    public async Task<UserAccount?> FindByEmail(Email Email)
    {
        var result = await _session.Query<UserAccount>().Where(x=>x.Email == Email.Value).SingleAsync();
        return result;
    }

    public async Task<UserAccount?> FindById(Guid Id)
    {
        var result = await _session.Query<UserAccount>().Where(x=>x.Id == Id).SingleAsync();
        return result;
    }
}