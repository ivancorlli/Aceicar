using IdContext.Core.Entity;
using IdContext.Core.Enumerable;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdContext.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // User
            builder.Entity<User>().Property(x => x.Status).HasConversion(x => x.ToString(), x => (UserStatus)Enum.Parse(typeof(UserStatus), x));
            builder.Entity<User>().Property(x => x.IsAuthenticatedExternaly).IsRequired();
            builder.Entity<User>().OwnsOne(x=>x.Profile,p=>{
                p.Property(x=>x.Name).HasColumnType("VARCHAR").HasMaxLength(25).IsRequired();
                p.Property(x=>x.Surname).HasColumnType("VARCHAR").HasMaxLength(25).IsRequired();
                p.Property(x=>x.Gender).HasColumnType("VARCHAR").HasMaxLength(10).IsRequired();
                p.Property(x=>x.Birth).IsRequired();
            });
            // Role
            builder.Entity<Role>().Property(x => x.Type).HasColumnType("VARCHAR").HasMaxLength(25);
        }
    }
}