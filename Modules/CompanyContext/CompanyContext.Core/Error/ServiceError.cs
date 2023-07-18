using Common.Basis.Error;

namespace CompanyContext.Core.Error;

public record ServiceExists : DomainError
{
    public ServiceExists() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext), nameof(ServiceExists)), "Service name already used") { }
}

public record ServiceNotFound : DomainError
{
    public ServiceNotFound() : base(ErrorTypes.TypeBuilder(nameof(CompanyContext), nameof(ServiceNotFound)), "Service not found") { }
}