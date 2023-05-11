using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Models.Domain;

namespace ProjectDetailsAPI.Data
{
    public class ProjectDetailsDbContext : DbContext
    {
    //    public ProjectDetailsDbContext()
    //    {
    //    }

        public ProjectDetailsDbContext(DbContextOptions<ProjectDetailsDbContext> options) : base(options)
        {
            
        }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
