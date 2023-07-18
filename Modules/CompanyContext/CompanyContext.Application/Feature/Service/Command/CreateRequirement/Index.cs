using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Service.Command.CreateRequirement;

public sealed record CreateRequirementCommand(Guid ServiceId,Guid CategoryId,Guid? SubCategoryId);
public static class CreateRequirementHandler
{
    public static async Task<IOperationResult> Handle(
        CreateRequirementCommand command,
        IEfWork session,
        CancellationToken cancellationToken
    )
    {
        CompanyContext.Core.Aggregate.Service? service = await session.ServiceRepository.GetById(command.ServiceId);
        if( service == null) return OperationResult.NotFound(new ServiceNotFound());
        if(command.SubCategoryId != null) 
        {
            Guid subcategory = command.SubCategoryId.Value;
            service.RequireProduct(command.CategoryId,subcategory);
        }else {
            service.RequireProduct(command.CategoryId);
        }
        session.ServiceRepository.Update(service);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
    
}