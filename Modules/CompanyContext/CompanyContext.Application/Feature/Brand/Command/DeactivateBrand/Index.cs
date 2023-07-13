using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Brand.Command.DeactivateBrand;

public sealed record DeactivateBrandCommand(Guid BrandId);
public static class DeactivateBrandHandler
{
    public static async Task<IOperationResult> Handle(
        DeactivateBrandCommand command,
        IEfWork session,
        CancellationToken cancellationToken
    )
    {
        ProductBrand? brand = await session.BrandRepository.GetById(command.BrandId);
        if(brand == null) return OperationResult.NotFound(new BrandNotFound());
        brand.Deactivate();
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
    
}