namespace CompanyContext.Core.ValueObject;

public sealed record Requirement
{
    public Guid CategoryId { get; private set; }
    /// <summary>
    /// If is null means this is available for all the category, but if it has a subcategory id it is only valid to that subcategory. 
    /// </summary>
    /// <value></value>
    public Guid? SubCategoryId { get; private set; }

    internal Requirement(Guid categoryId)
    {
        CategoryId = categoryId;
    }
    internal Requirement(Guid categoryId, Guid subCategoryId)
    {
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
    }
}