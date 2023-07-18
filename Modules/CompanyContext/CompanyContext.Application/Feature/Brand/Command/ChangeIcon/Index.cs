using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Brand.Command.ChangeIcon;

public sealed record ChangeLogoCommand(Guid BrandId,string Logo);
public static class ChangeLogoHandler
{
    public static async Task<IOperationResult> Handle(
        ChangeLogoCommand command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        CompanyContext.Core.Aggregate.Brand? brand = await session.BrandRepository.FindById(command.BrandId);
        if(brand == null) return OperationResult.NotFound(new BrandNotFound());
        brand.ChangeLogo(command.Logo);
        session.BrandRepository.Apply(brand);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}