using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services.IProjects;

namespace ProjectDetailsAPI.Implementation.ProjectsImp
{
    public class AddProjectsRepository :  IAddProjectsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ProjectDetailsDbContext _dbcontext;

        public AddProjectsRepository(IConfiguration configuration,ProjectDetailsDbContext dbcontext)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
        }

        public async Task<Projects> AddProjects(Projects projects)
        {
            await _dbcontext.Projects.AddAsync(projects);
            await _dbcontext.SaveChangesAsync();
            return projects;
        }

        public async Task<Projects> DeleteProjectById(int id)
        {
            var existingProject = await _dbcontext.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProject == null)
            {
                return null;
            }

            existingProject.isDeleted = true;

            await _dbcontext.SaveChangesAsync();

            return existingProject;
        }

        public async Task<Projects> UpdateProject(int id, Projects projects)
        {

            var existingProject = await _dbcontext.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProject == null)
            {
                return null;
            }
            existingProject.ProjectName = projects.ProjectName;
            existingProject.projectManager = projects.projectManager;
            existingProject.projectCost = projects.projectCost;
            existingProject.ratePerHour = projects.ratePerHour;
            existingProject.description = projects.description;
            existingProject.ClientName = projects.ClientName;

            await _dbcontext.SaveChangesAsync();

            return existingProject;
        }
    }
}
