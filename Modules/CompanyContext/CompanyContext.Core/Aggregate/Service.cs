using CompanyContext.Core.Enumerable;
using CompanyContext.Core.Interface;
using CompanyContext.Core.ValueObject;

namespace CompanyContext.Core.Aggregate;

public sealed class Service : IUseArea
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public ServiceStatus Status { get; private set; } = default!;
    private IList<Requirement> _requires { get; set; } = new List<Requirement>();
    public IEnumerable<Requirement> Requires => _requires;

    internal Service(string name)
    {
        Id = Guid.NewGuid();
        Name = name.Trim().ToLower();
        Status = ServiceStatus.Active;
    }

    public void RequireProduct(Guid categoryId)
    {
        // search if exist any 
        IList<Requirement> exists = _requires.Where(x => x.CategoryId == categoryId).ToList();
        if (exists.Count > 0)
        {
            // Search if there is on unique
            Requirement? type = _requires.Where(x => x.CategoryId == categoryId && x.SubCategoryId == null).SingleOrDefault();
            // If there is not one unique, get all the subcategories and delete it
            if (type == null)
            {
                IList<Requirement> products = exists.Where(x => x.CategoryId == categoryId && x.SubCategoryId != null).ToList();
                if (products.Count > 0)
                {
                    foreach (Requirement item in products)
                    {
                        _requires.Remove(item);
                    }
                }
                // Add unique product 
                Requirement product = new Requirement(categoryId);
                _requires.Add(product);
            }
        }
        else
        {
            _requires.Add(new Requirement(categoryId));
        }
    }

    public void RequireProduct(Guid categoryId, Guid subCategoryId)
    {
        // Search if exists any subcategory related with the service
        IList<Requirement> exist = _requires.Where(x => x.CategoryId == categoryId).ToList();
        if (exist.Count > 0)
        {
            // Search if there is one category realted and delete it
            Requirement? category = _requires.Where(x => x.CategoryId == categoryId && x.SubCategoryId == null).SingleOrDefault();
            if (category != null)
            {
                _requires.Remove(category);
            }
            // search if exists the same subcategory
            Requirement? subcategory = _requires.Where(x => x.CategoryId == categoryId && x.SubCategoryId == subCategoryId).SingleOrDefault();
            if (subcategory == null)
            {
                _requires.Add(new Requirement(categoryId, subCategoryId));
            }

        }
        else
        {
            _requires.Add(new Requirement(categoryId, subCategoryId));
        }
    }

    public override void AddToSpecialization(Guid typeId, Guid specializationId)
    {
        // Search if exists any type and specialization related with the category
        IList<IArea> exist = _areas.Where(x => x.TypeId == typeId).ToList();
        if (exist.Count > 0)
        {
            // Search if there is one type unique realated to the category
            IArea? type = _areas.Where(x => x.TypeId == typeId && x.SpecializationId == null).SingleOrDefault();
            if (type != null)
            {
                _areas.Remove(type);
            }
            // search if exists the same specialization
            IArea? specialization = _areas.Where(x => x.TypeId == typeId && x.SpecializationId == specializationId).SingleOrDefault();
            if (specialization == null)
            {
                _areas.Add(new ServiceArea(typeId, specializationId));
            }

        }
        else
        {
            _areas.Add(new ServiceArea(typeId, specializationId));
        }
    }

    public override void AddToType(Guid typeId)
    {
        // Search if exists any type and specialization related with the category
        IList<IArea> exist = _areas.Where(x => x.TypeId == typeId).ToList();
        if (exist.Count > 0)
        {
            // Search if there is one type unique realated to the category
            IArea? type = _areas.Where(x => x.TypeId == typeId && x.SpecializationId == null).SingleOrDefault();
            // If there is not a type, search if there is a specialization
            if (type == null)
            {
                IList<IArea> specializations = exist.Where(x => x.TypeId == typeId && x.SpecializationId != null).ToList();
                if (specializations.Count > 0)
                {
                    foreach (IArea item in specializations)
                    {
                        _areas.Remove(item);
                    }
                }
                // Add category to a specific type
                ServiceArea area = new ServiceArea(typeId);
                _areas.Add(area);
            }
        }
        else
        {
            // Add category to a specific type
            _areas.Add(new ServiceArea(typeId));
        }
    }
}