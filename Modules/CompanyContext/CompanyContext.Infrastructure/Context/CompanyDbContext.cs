using CompanyContext.Core.Aggregate;
using CompanyContext.Core.Entity;
using CompanyContext.Core.Interface;
using CompanyContext.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyContext.Infrastructure.Context;

public class CompanyDbContext : DbContext
{
    public DbSet<IArea> Area => Set<IArea>();
    public DbSet<Core.Aggregate.Type> Type => Set<Core.Aggregate.Type>();
    public DbSet<Specialization> Specialization => Set<Specialization>();
    public DbSet<Category> Category => Set<Category>();
    public DbSet<SubCategory> SubCategory => Set<SubCategory>();
    public DbSet<Service> Service => Set<Service>(); 
    public CompanyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AreaConfiguration());
        modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
        modelBuilder.ApplyConfiguration(new TypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new SpecializationConfiguration());
        modelBuilder.ApplyConfiguration(new SubCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryAreaConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceAreaConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceConfiguration());
    }
}