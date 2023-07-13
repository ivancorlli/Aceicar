using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Brand.Command.ChangeIcon;

public sealed record ChangeLogoCommand(Guid BrandId,string Icon);
public static class ChangeLogoHandler
{
    public static async Task<IOperationResult> Handle(
        ChangeLogoCommand command,
        IEfWork session,
        CancellationToken cancellationToken
    )
    {
        ProductBrand? brand = await session.BrandRepository.GetById(command.BrandId);
        if(brand == null) return OperationResult.NotFound(new BrandNotFound());
        brand.ChangeIcon(command.Icon);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}