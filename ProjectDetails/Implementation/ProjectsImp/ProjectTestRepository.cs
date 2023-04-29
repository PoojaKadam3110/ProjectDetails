using MySqlX.XDevAPI;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services.IProjects;

namespace ProjectDetailsAPI.Implementation.ProjectsImp
{
    public class ProjectTestRepository : GenericRepository<Projects>, IProjectTestRepository
    {
        public ProjectTestRepository(ProjectDetailsDbContext dbcontext): base(dbcontext) 
        {
        }       
    }
}
