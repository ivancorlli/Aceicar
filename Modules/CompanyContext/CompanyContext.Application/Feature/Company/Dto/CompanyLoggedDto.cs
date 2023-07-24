using CompanyContext.Application.ViewModel;

namespace CompanyContext.Application.Feature.Company.Dto;


public sealed record CompanyLoggedContactData(string Email, string Country, string Number);
public sealed record CompanyLoggedLocation(
string Country,
 string City,
 string State,
 string PostalCode,
 string Street,
 string StreetNumber,
 string? Floor,
 string? Apartment
);
public sealed record CompanyLoggedDto
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = default!;
    public bool Published { get; set; } = default!;
    public string? Picture { get; set; }
    public string? Description { get; set; }
    public CompanyLoggedContactData? ContactData { get; set; }
    public CompanyLoggedLocation? Location { get; set; }
}

public static class MapCompanyLogged
{
    public static CompanyLoggedDto Map(
        CompanyProjection company
    )
    {
        return new CompanyLoggedDto()
        {
            CompanyId = company.CompanyId,
            Name = company.Name,
            Published = company.Published,
            Picture = company.Picture,
            Description = company.Description,
            ContactData = company.Email != null && company.PhoneCountry != null && company.PhoneNumber != null ? new CompanyLoggedContactData(company.Email, company.PhoneCountry, company.PhoneNumber) : null,
            Location = company.Country != null && company.City != null && company.State != null && company.PostalCode != null && company.Street != null && company.StreetNumber != null ? new CompanyLoggedLocation(company.Country, company.City, company.State, company.PostalCode, company.Street, company.StreetNumber, company.Floor, company.Apartment) : null
        };
    }
}