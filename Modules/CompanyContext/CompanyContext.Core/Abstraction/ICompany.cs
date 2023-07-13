using Common.Basis.Aggregate;
using Common.Basis.ValueObject;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Enumerable;
using CompanyContext.Core.ValueObject;

namespace CompanyContext.Core.Abstraction;

public abstract class ICompany:IAggregate
{
    public Guid Owner {get;protected set;} = default!;
    public CompanyName Name {get;protected set;} = default!;
    public Email Email {get;protected set;} = default!;
    public Phone? Phone {get;protected set;} = default!;
    public CompanyLogo? Logo {get;protected set;} = default!;
    public CompanyDescription? Description {get;protected set;} = default!;
    public Location Address {get; protected set;} = default!;
    public CompanyStatus Status {get;protected set;} = default!;
    public bool Published {get;protected set;} = false;
    public DateTime? PublishedAt {get;protected set;} = default!;
    private IList<CompanyArea> _areas = new List<CompanyArea>();
    private IList<Employee> _employees = new List<Employee>();
    public IEnumerable<CompanyArea> Areas => _areas.AsReadOnly();
    public IEnumerable<Employee> Employees => _employees.AsReadOnly();
}