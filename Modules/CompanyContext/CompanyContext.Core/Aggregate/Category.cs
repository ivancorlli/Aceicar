using Common.Basis.Utils;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Enumerable;
using CompanyContext.Core.Interface;
using CompanyContext.Core.ValueObject;

namespace CompanyContext.Core.Aggregate;

public sealed class Category
{
    public Guid Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public CategoryStatus Status { get; private set; } = default!;
    private IList<SubCategory> _subcategories { get; set; } = new List<SubCategory>();
    public IEnumerable<SubCategory> SubCategories => _subcategories;
    private IList<CategoryArea> _areas { get; set; } = new List<CategoryArea>();
    public IEnumerable<CategoryArea> Areas => _areas;


    internal Category(string name)
    {
        Id = Guid.NewGuid();
        Name = name.Trim().ToLower();
        Status = CategoryStatus.Active;
    }

    /// <summary>
    /// Add new Subcategory to categories for products 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Result<SubCategory> CreateSubCategory(string name)
    {
        IList<SubCategory> exists = _subcategories.Where(x => x.Name.ToLower() == name.Trim().ToLower()).ToList();
        if (exists.Count <= 0)
        {
            Result<SubCategory> newSub = SubCategory.Create(name);
            if (newSub.IsFailure) return Result.Fail<SubCategory>(newSub.Error);
            _subcategories.Add(newSub.Value);
            return Result.Ok<SubCategory>(newSub.Value);
        }
        return Result.Ok<SubCategory>(exists.First());
    }

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
                CategoryArea area = new CategoryArea(typeId);
                _areas.Add(area);
            }
        }
        else
        {
            // Add category to a specific type
            _areas.Add(new CategoryArea(typeId));
        }
    }

    public void AddToSpecialization(Guid typeId, Guid specializationId)
    {
        // Search if exists any type and specialization related with the category
        IList<CategoryArea> exist = _areas.Where(x => x.TypeId == typeId).ToList();
        if (exist.Count > 0)
        {
            // Search if there is one type unique realated to the category
            CategoryArea? type = _areas.Where(x => x.TypeId == typeId && x.SpecializationId == null).SingleOrDefault();
            if (type != null)
            {
                _areas.Remove(type);
            }
            // search if exists the same specialization
            CategoryArea? specialization = _areas.Where(x => x.TypeId == typeId && x.SpecializationId == specializationId).SingleOrDefault();
            if (specialization == null)
            {
                _areas.Add(new CategoryArea(typeId, specializationId));
            }

        }
        else
        {
            _areas.Add(new CategoryArea(typeId, specializationId));
        }
    }
}