using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Event;
using CompanyContext.Core.Repository;
using CompanyContext.Core.Service;

namespace CompanyContext.Application.Feature.Brand.Command.CreateBrand;

public sealed record CreateBrandCommand(string Name,string? Logo);
public static class CreateBrandHandler
{
    public static async Task<IOperationResult> Handle(
        CreateBrandCommand command,
        IUoW session,
        CancellationToken cancellationToken,
        BrandManager manager
    )
    {
        BrandCreated @event = new(Guid.NewGuid(),command.Name,command.Logo);
        Result<CompanyContext.Core.Aggregate.Brand> brand = await manager.Create(@event);
        if(brand.IsFailure) return OperationResult.Invalid(brand.Error);
        session.BrandRepository.Create(brand.Value.Id,@event);
        await session.SaveChangesAsync(cancellationToken); 
        return OperationResult.Success();
    }
    
}