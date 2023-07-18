using Common.Basis.Aggregate;

namespace CompanyContext.Core.Interface;

public abstract class IProduct:IAggregate
{
    public Guid CategoryId {get; protected set;} = default!;
    public Guid? SubCategoryId {get; protected set;} = default!;
    public Guid? BrandId {get; protected set;} = default!;
    public string Code {get; protected set;} = default!;
    public string Name {get; protected set;} = default!;
    public string? Description {get; protected set;} = default!;
    private IList<string> _images {get;set;} = new List<string>();
    public IEnumerable<string> Images => _images.AsReadOnly();   
}