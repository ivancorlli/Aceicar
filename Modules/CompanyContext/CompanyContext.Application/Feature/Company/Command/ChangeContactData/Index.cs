using Common.Basis.Interface;
using Common.Basis.Utils;
using Common.Basis.ValueObject;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Company.Command.ChangeContactData;

public sealed record ChangeContactDataCommand(Guid CompanyId,string Email,string Country,string Number);
public static class ChangeContactDataHandler
{
    public static async Task<IOperationResult> Handle(
        ChangeContactDataCommand command,
        IUoW session,
         CancellationToken cancellationToken
    )
    {
        CompanyContext.Core.Aggregate.Company? company = await session.CompanyRepository.FindById(command.CompanyId); 
        if(company == null) return OperationResult.NotFound(new CompanyNotFound());
        Email email = Email.Create(command.Email);
        Phone phone = Phone.Create(command.Country,command.Number);
        company.ChangeEmail(email);
        company.ChangePhone(phone);
        session.CompanyRepository.Apply(company);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}