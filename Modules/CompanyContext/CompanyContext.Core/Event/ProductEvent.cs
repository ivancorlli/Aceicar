namespace CompanyContext.Core.Event;

public sealed record ProductCreatedForCategory(Guid ProductId, string Code, string Name, Guid CategoryId, Guid? CompanyId = null);
public sealed record ProductCreatedForSubcategory(Guid ProductId, string Code, string Name, Guid CategoryId, Guid SubCategoryId, Guid? CompanyId = null);
public sealed record DescriptionAdded(Guid ProductId,string Description);
public sealed record AddedToBrand(Guid ProducId,Guid BrandId);
public sealed record ImageAdded(Guid ProductId, string Image);
public sealed record ImageDeleted(Guid ProductId, string Image);