using CompanyContext.Core.ValueObject;

namespace CompanyContext.Core.Aggregate;

public sealed class ProductCategory
{
    public Guid Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    private IList<CategoryArea> _areas { get; set; } = new List<CategoryArea>();
    public IEnumerable<CategoryArea> Areas => _areas.AsReadOnly();

    internal ProductCategory(string name)
    {
        Id = Guid.NewGuid();
        Name = name.Trim().ToLower();
    }

    /// <summary>
    /// Add a category to a company type, if there is any specialization it will remove all the specializations and add the type. 
    /// </summary>
    /// <param name="typeId"></param>
    public void AddToType(Guid typeId)
    {
        // Search if exists any type and specialization related with the category
        IList<CategoryArea> exist = _areas.Where(x => x.TypeId == typeId).ToList();
        if (exist.Count > 0)
        {
            // Search if there is one type unique realated to the category
            CategoryArea? type = _areas.Where(x => x.TypeId == typeId && x.SpecializationId == null).SingleOrDefault();
            // If there is not a type, search if there is a specialization
            if (type == null)
            {
                IList<CategoryArea> specializations = exist.Where(x => x.TypeId == typeId && x.SpecializationId != null).ToList();
                if (specializations.Count > 0)
                {
                    foreach (CategoryArea item in specializations)
                    {
                        _areas.Remove(item);
                    }
                }
                // Add category to a specific type
                _areas.Add(CategoryArea.InType(typeId));
            }
        }
        else
        {
            // Add category to a specific type
            _areas.Add(CategoryArea.InType(typeId));
        }
    }

    /// <summary>
    /// Add a category to a specialization, if it was added to a company type it will be removed and it will be added to a specific specialization.  
    /// </summary>
    public void AddToSpecialization(Guid typeId, Guid specializationId)
    {
        // Search if exists any type and specialization related with the category
        IList<CategoryArea> exist = _areas.Where(x => x.TypeId == typeId).ToList();
        if (exist.Count > 0)
        {
            // Search if there is one type unique realated to the category
            CategoryArea? type = _areas.Where(x => x.TypeId == typeId && x.SpecializationId == null).SingleOrDefault();
            if(type != null)
            {
                _areas.Remove(type);
            }
            // search if exists the same specialization
            CategoryArea? specialization = _areas.Where(x=>x.TypeId ==typeId && x.SpecializationId == specializationId).SingleOrDefault();
            if(specialization == null)
            {
                _areas.Add(CategoryArea.InSpecialization(typeId,specializationId));
            }

        }
        else
        {
            _areas.Add(CategoryArea.InSpecialization(typeId,specializationId));
        }
    }

}