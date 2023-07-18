using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Entity;
using CompanyContext.Core.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyContext.Infrastructure.Data;

public class CategoryAreaConfiguration : IEntityTypeConfiguration<CategoryArea>
{
    public void Configure(EntityTypeBuilder<CategoryArea> builder)
    {
        builder.HasOne<Core.Aggregate.Type>().WithMany().HasForeignKey(x=>x.TypeId);
        builder.HasOne<Specialization>().WithMany().HasForeignKey(x=>x.SpecializationId);
    }
}