using CompanyContext.Core.Repository;

namespace CompanyContext.Application.Feature.Type.Query.TypeById;

public sealed record TypeByIdQuery(Guid TypeId);
public static class TypeByIdHandler
{
    public static Task<CompanyContext.Core.Aggregate.Type?> Handle(
        TypeByIdQuery query,
        IEfWork session,
        CancellationToken cancellationToken
    )   
    {
        return session.TypeRepository.FindById(query.TypeId);
    }
}