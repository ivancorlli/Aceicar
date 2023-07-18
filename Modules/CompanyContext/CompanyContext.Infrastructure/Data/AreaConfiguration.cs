using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyContext.Infrastructure.Data;

public sealed class AreaConfiguration : IEntityTypeConfiguration<IArea>
{
    public void Configure(EntityTypeBuilder<IArea> builder)
    {
        builder.Property<int>("Id");
        builder.HasKey("Id");
        builder.HasDiscriminator();
        builder.HasOne<Core.Aggregate.Type>().WithMany().HasForeignKey(x=>x.TypeId);
        builder.HasOne<Specialization>().WithMany().HasForeignKey(x=>x.SpecializationId);    
    }
}