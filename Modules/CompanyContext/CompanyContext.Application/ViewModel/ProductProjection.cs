namespace CompanyContext.Application.ViewModel;

public sealed class ProductProjection
{
    public Guid ProductId {get;set;}
    public string Code {get; set;} = default!;
    public string Name {get; set;} = default!;
    public Guid CategoryId {get;set;} = default!;
    /// <summary>
    /// If it is null means product is available for all categories 
    /// </summary>
    public Guid? SubCategoryId {get;set;} =default!;
    public Guid? BrandId {get;set;} =default!;
    /// <summary>
    /// If it is not null it means it is onli valid for a company 
    /// </summary>
    public Guid? CompanyId {get;set;} = default!;
    public string? Description {get;set;} = default!;
    public IList<string> Images {get;set;} = new List<string>();
} 