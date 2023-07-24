
using CompanyContext.Core.Event;
using CompanyContext.Core.Interface;
using CompanyContext.Core.ValueObject;

namespace CompanyContext.Core.Aggregate;

public sealed class Company : ICompany
{

    public static Company Create(CompanyCreated @event)
    {
        Company newCompany = new Company()
        {
            Id = @event.CompanyId,
            Name = CompanyName.Create(@event.Name),
            Status = Enumerable.CompanyStatus.Active
        };
        return newCompany;
    }

}