using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.GenericRepo;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;

namespace ProjectDetailsAPI.Implementation
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly ProjectDetailsDbContext _dbcontext;
        public RoleRepository(ProjectDetailsDbContext projectDetailsDbContext) : base(projectDetailsDbContext)
        {
            _dbcontext = projectDetailsDbContext;
        }
    }
}
