using CompanyContext.Core.Enumerable;

namespace CompanyContext.Application.ViewModel;

public sealed class UserAccess
{
    public Guid AccessId {get;set;}
    public Guid UserId {get;set;}
    public Guid CompanyId {get;set;}
    public Guid RoleId {get;set;}
    public AccessStatus Status {get;set;}
    public DateTimeOffset CreatedAt {get;set;}
    public DateTimeOffset UpdatedAt {get;set;}
    public void Update() => UpdatedAt = DateTimeOffset.Now; 
}