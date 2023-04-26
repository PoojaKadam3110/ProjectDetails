using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;

namespace ProjectDetailsAPI.Implementation
{
    public class ProjectsRepository : GenericRepository<Projects>, IProjectsRepository
    {
        private readonly ProjectDetailsDbContext _dbcontext;
        public ProjectsRepository(ProjectDetailsDbContext projectDetailsDbContext) : base(projectDetailsDbContext)
        {
            _dbcontext = projectDetailsDbContext;
        }
    }
}
