using CompanyContext.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyContext.Infrastructure.Data;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.HasKey(x=>x.Id);
        // Properties
        builder.Property(x=>x.Name).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);
        builder.Property(x=>x.Status).IsRequired();
        builder.Property(x=>x.SpecializationIcon).IsRequired(false).HasColumnType("VARCHAR").HasMaxLength(500);
    }
}