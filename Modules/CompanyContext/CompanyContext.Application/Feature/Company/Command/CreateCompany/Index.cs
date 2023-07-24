using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Event;
using CompanyContext.Core.Repository;
using CompanyContext.Core.Service;

namespace CompanyContext.Application.Feature.Company.Command.CreateCompany;

public sealed record CreateCompanyCommand(Guid TypeId ,Guid SpecializationId,Guid Owner,string Name);
public static class CreateCompanyHandler
{
    public static async Task<IOperationResult> Handle(
        CreateCompanyCommand command,
        CompanyManager manager,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        CompanyCreated @event = new(Guid.NewGuid(),command.Owner,command.Name.Trim().ToLower());
        Result<CompanyContext.Core.Aggregate.Company> result = await manager.Create(@event,command.TypeId,command.SpecializationId);
        if(result.IsFailure) return OperationResult.Invalid(result.Error);
        session.CompanyRepository.Create(result.Value.Id,@event);
        session.CompanyRepository.Apply(result.Value);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}