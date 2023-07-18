
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CompanyContext.Infrastructure.Context;

public class DbContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
{
public CompanyDbContext CreateDbContext(string[] args)
{ 
    var optionsBuilder = new DbContextOptionsBuilder<CompanyDbContext>();
    optionsBuilder.UseNpgsql(args[0].ToString().Trim(),x=>x.EnableRetryOnFailure());

    return new CompanyDbContext(optionsBuilder.Options);
}
}