using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyContext.Infrastructure.Data;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.HasIndex(x=>x.Name);
        // Properties
        builder.Property(x=>x.Name).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);
        builder.Property(x=>x.Status).IsRequired();
        builder.OwnsMany(x=>x.Areas,area=>{
            area.Property(x=>x.TypeId).IsRequired();
            area.Property(x=>x.SpecializationId).IsRequired(false);
        });
    }
}