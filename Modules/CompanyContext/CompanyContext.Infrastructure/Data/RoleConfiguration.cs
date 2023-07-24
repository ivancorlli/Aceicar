using CompanyContext.Core.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyContext.Infrastructure.Data;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Name).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);
        builder.Property(x=>x.Status).IsRequired();
    }
}