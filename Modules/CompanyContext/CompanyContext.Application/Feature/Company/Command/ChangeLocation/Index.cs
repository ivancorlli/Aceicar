using Common.Basis.Interface;
using Common.Basis.Utils;
using CompanyContext.Core.Error;
using CompanyContext.Core.Repository;
using CompanyContext.Core.ValueObject;

namespace CompanyContext.Application.Feature.Company.Command.ChangeLocation;

public sealed record ChangeLocationCommand(
    Guid CompanyId,
    string Country,
    string City,
    string State,
    string PostalCode,
    string Street,
    string StreetNumber,
    string? Floor,
    string? Aprtment
);
public static class ChangeLocationHandler
{
    public static async Task<IOperationResult> Handle(
        ChangeLocationCommand command,
        IUoW session,
        CancellationToken cancellationToken
    )
    {
        Location? location = null;
        if (command.Floor != null && command.Aprtment != null)
        {
            location = Location.Create(
                command.Country,
                command.City,
                command.State,
                command.PostalCode,
                command.Street,
                command.StreetNumber,
                command.Floor,
                command.Aprtment
            );

        }
        else
        {
            location = Location.Create(
                command.Country,
                command.City,
                command.State,
                command.PostalCode,
                command.Street,
                command.StreetNumber
            );
        }
        CompanyContext.Core.Aggregate.Company? company = await session.CompanyRepository.FindById(command.CompanyId);
        if (company == null) return OperationResult.NotFound(new CompanyNotFound());
        company.ChangeLocation(location);
        session.CompanyRepository.Apply(company);
        await session.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();

    }
}