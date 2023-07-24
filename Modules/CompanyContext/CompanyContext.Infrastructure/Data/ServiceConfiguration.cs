using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyContext.Infrastructure.Data;

public sealed class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.HasIndex(x=>x.Name);
        builder.Property(x=>x.Name).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);
        builder.Property(x=>x.Status).IsRequired();
        builder.OwnsMany(x=>x.Requirments,req=>{
            req.HasOne<Category>().WithMany().HasForeignKey(x=>x.CategoryId);
            req.HasOne<SubCategory>().WithMany().HasForeignKey(x=>x.SubCategoryId);
        });
    }
}