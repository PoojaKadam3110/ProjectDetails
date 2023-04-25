using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectDetailsAPI.Data
{
    public class ProjectDetailsAuthDbContext : IdentityDbContext
    {
        public ProjectDetailsAuthDbContext(DbContextOptions<ProjectDetailsAuthDbContext> options): base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "8ce10047-21fe-4137-bf03-1f3013b7b33e";
            var writerRoleId = "0875f00e-c427-4a7a-bd2c-0cd8454690a7";

            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name= "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp= writerRoleId,
                    Name= "Client",
                    NormalizedName = "Client".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
