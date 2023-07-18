using CompanyContext.Core.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyContext.Infrastructure.Data;

public class TypeConfiguration : IEntityTypeConfiguration<Core.Aggregate.Type>
{
    public void Configure(EntityTypeBuilder<Core.Aggregate.Type> builder)
    {
        builder.HasKey(x=>x.Id);

        // Propiedades 
        builder.Property(x=>x.Name).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);
        builder.Property(x=>x.TypeIcon).IsRequired(false).HasColumnType("VARCHAR").HasMaxLength(500);
        builder.Property(x=>x.Status).IsRequired();
    }
}