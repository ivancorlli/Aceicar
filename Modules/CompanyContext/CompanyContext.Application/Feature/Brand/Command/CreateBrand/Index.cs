using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Repository;
using CompanyContext.Core.Service;

namespace CompanyContext.Application.Feature.Brand.Command.CreateBrand;

public sealed record CreateBrandCommand(string Name,string? Icon);
public static class CreateBrandHandler
{
    public static async Task<IOperationResult> Handle(
        CreateBrandCommand command,
        IEfWork session,
        CancellationToken cancellationToken,
        BrandManager manager
    )
    {
        Result<ProductBrand> brand = await manager.CreateBrand(command.Name);
        if(brand.IsFailure) return OperationResult.Invalid(brand.Error);
        if(command.Icon != null)
        {
            brand.Value.ChangeIcon(command.Icon);
        }
        session.BrandRepository.Create(brand.Value);
        await session.SaveChangesAsync(cancellationToken); 
        return OperationResult.Success();
    }
    
}