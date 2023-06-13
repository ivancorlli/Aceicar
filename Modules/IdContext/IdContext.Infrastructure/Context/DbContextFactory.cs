using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdContext.Infrastructure.Context;

public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    { 
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(args[0].ToString().Trim(),x=>x.EnableRetryOnFailure());
        optionsBuilder.UseOpenIddict();

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}