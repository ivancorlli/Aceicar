using Common.Basis.Utils;
using CompanyContext.Core.Enumerable;

namespace CompanyContext.Core.Entity;

public sealed class SubCategory
{
    public Guid Id {get;private set;}
    public string Name {get;private set;} =default!;
    public CategoryStatus Status {get;private set;} = default!;   

    internal static Result<SubCategory> Create(string name)
    {
        SubCategory newSub = new SubCategory();
        newSub.Id = Guid.NewGuid();
        newSub.Name = name.Trim().ToLower();
        newSub.Status = CategoryStatus.Active;
        return Result.Ok(newSub);   
    }

    public void Deactivate()
    {
        Status = CategoryStatus.Inactive;
    }
    public void Delete()
    {
        Status = CategoryStatus.Deleted;
    }

}