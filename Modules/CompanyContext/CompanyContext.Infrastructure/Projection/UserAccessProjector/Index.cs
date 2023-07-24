using CompanyContext.Application.ViewModel;
using CompanyContext.Core.Event;
using Marten.Events.Aggregation;

namespace CompanyContext.Infrastructure.Projection.UserAccessProjector;

public sealed class UserAccessProjector : SingleStreamProjection<UserAccess>
{

    public UserAccess Create(AccessCreated @event)
    {
        DateTimeOffset time = DateTimeOffset.UtcNow;
        return new UserAccess()
        {
            AccessId = @event.AccessId,
            CompanyId = @event.CompanyId,
            UserId = @event.UserId,
            RoleId = @event.RoleId,
            Status = Core.Enumerable.AccessStatus.Active,
            CreatedAt = time,
            UpdatedAt = time,
        };
    }
}