using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Brand.Command.DeactivateBrand;

public sealed record DeactivateBrandCommand(Guid BrandId);
public static class DeactivateBrandHandler
{
    public static async Task<IOperationResult> Handle(
        DeactivateBrandCommand command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        CompanyContext.Core.Aggregate.Brand? brand = await session.BrandRepository.FindById(command.BrandId);
        if(brand == null) return OperationResult.NotFound(new BrandNotFound());
        brand.Deactivate();
        session.BrandRepository.Apply(brand);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
    
}