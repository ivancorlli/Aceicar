using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Event;
using CompanyContext.Core.Repository;
using CompanyContext.Core.Service;

namespace CompanyContext.Application.Feature.Brand.Command.CreateBrandForCompany;

public sealed record CreateBrandForCompanyCommand(string Name,Guid CompanyId, string? Logo);
public static class CreateBrandForCompanyHandler
{
    public static async Task<IOperationResult> Handle(
        CreateBrandForCompanyCommand command,
        IUoW session,
        CancellationToken cancellationToken,
        BrandManager manager
    )
    {
        BrandForCompanyCreated @event = new(Guid.NewGuid(), command.Name,command.CompanyId ,command.Logo);
        Result<CompanyContext.Core.Aggregate.Brand> brand = await manager.Create(@event);
        if (brand.IsFailure) return OperationResult.Invalid(brand.Error);
        session.BrandRepository.Create(brand.Value.Id, @event);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }

}