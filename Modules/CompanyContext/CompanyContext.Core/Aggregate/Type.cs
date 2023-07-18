using Common.Basis.Utils;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Enumerable;

namespace CompanyContext.Core.Aggregate;

public sealed class Type
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public TypeStatus Status { get; private set; }
    public string? TypeIcon { get; private set; }
    private IList<Specialization> _specializations = new List<Specialization>();
    public IEnumerable<Specialization> Specializations => _specializations;

    internal Type(string name)
    {
        Name = name.ToLower().Trim();
        Status = TypeStatus.Active;
    }

    public void Activate()
    {
        Status = TypeStatus.Active;
    }

    public void Deactivate()
    {
        Status = TypeStatus.Inactive;
    }

    public void ChangeIcon(string icon)
    {
        TypeIcon = icon.Trim();
    }

    /// <summary>
    /// Add specialization to type 
    /// </summary>
    /// <param name="name"></param>
    public Result<Specialization> AddSpecialization(string name)
    {
        IList<Specialization> exist = _specializations.Where(x => x.Name == name.Trim().ToLower()).ToList();
        if (exist.Count <= 0)
        {
            Result<Specialization> specialization = Specialization.Create(name);

            if (specialization.IsFailure) return Result.Fail<Specialization>(specialization.Error);
            _specializations.Add(specialization.Value);
            return Result.Ok<Specialization>(specialization.Value);
        }
        return Result.Ok<Specialization>(exist.First());
    }

}