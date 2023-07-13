using Marten;
using Microsoft.Extensions.Configuration;
using UserContext.Application.Service;
using UserContext.Application.ViewModel;
using Common.Basis.ValueObject;

namespace UserContext.Infrastructure.Service;

public class UserAccountService : IUserAccountService
{
    private readonly IQuerySession _query;
    public UserAccountService(
        // IDocumentStore store
        IConfiguration configuration
        )
    {
        var store = DocumentStore.For(x =>
        {
            x.Connection(configuration.GetConnectionString("UserContextDb")!);
        });
        _query = store.QuerySession("");
    }

    public async Task<UserAccount?> FindByEmail(Email Email)
    {
        var result = await _query.Query<UserAccount>().Where(x => x.Email == Email.Value).ToListAsync();
        if (result.Count > 0) return result.SingleOrDefault();
        return null;
    }

    public async Task<UserAccount?> FindById(Guid Id)
    {
        var result = await _query.Query<UserAccount>().Where(x => x.Id == Id).SingleAsync();
        return result;
    }
}