namespace CompanyContext.Core.Interface;

public abstract class IUseArea
{
    protected IList<IArea> _areas { get; set; } = new List<IArea>();
    public IEnumerable<IArea> Areas => _areas;

    /// <summary>
    /// Add a category to a company type, if there is any specialization it will remove all the specializations and add the type. 
    /// </summary>
    /// <param name="typeId"></param>
    public abstract void AddToType(Guid typeId);
    /// <summary>
    /// Add a category to a specialization, if it was added to a company type it will be removed and it will be added to a specific specialization. 
    /// </summary>
    /// <param name="typeId"></param>
    /// <param name="specializationId"></param>
    public abstract void AddToSpecialization(Guid typeId, Guid specializationId);
}