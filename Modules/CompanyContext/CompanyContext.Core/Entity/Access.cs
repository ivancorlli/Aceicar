using Common.Basis.Utils;
using CompanyContext.Core.Enumerable;
using CompanyContext.Core.Error;

namespace CompanyContext.Core.Entity;

public sealed class Access
{
    public Guid Id {get; private set;} = default!;
    public Guid UserId {get; private set;} = default!;
    public Guid RoleId {get; private set;} = default!;
    public AccessStatus Status {get; private set;} = default!;

    internal Access(Guid id,Guid userId,Guid roleId)
    {
        Id = id;
        UserId = userId;
        RoleId = roleId;
        Status = AccessStatus.Active;
    }

    public Result ChangeRole(Guid roleId)
    {
        if(Status != AccessStatus.Deleted)
        {
            RoleId = roleId;
            return Result.Ok();
        }
        return Result.Fail(new AccessIsDeleted());
    }

    public void Deactivate()
    {
        Status = AccessStatus.Inactive;
    }
    public void Delete()
    {
        Status = AccessStatus.Deleted;
    }

}