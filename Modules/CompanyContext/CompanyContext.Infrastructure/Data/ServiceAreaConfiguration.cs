using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Entity;
using CompanyContext.Core.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyContext.Infrastructure.Data;

public sealed class ServiceAreaConfiguration : IEntityTypeConfiguration<ServiceArea>
{
    public void Configure(EntityTypeBuilder<ServiceArea> builder)
    {
        builder.HasOne<Core.Aggregate.Type>().WithMany().HasForeignKey(x=>x.TypeId);
        builder.HasOne<Specialization>().WithMany().HasForeignKey(x=>x.SpecializationId);
    }
}