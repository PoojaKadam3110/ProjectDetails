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
        public async Task<List<Projects>> GetAll(string? filterOn = null, string? filterQuery = null,
          string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)  //modified thid method for filtering and middle two for sorting and last two for pagginatiom
        {
            var walks = _dbcontext.Projects.AsQueryable();
            //Filterring
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Project Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.ProjectName.Contains(filterQuery));
                }
            }

            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.CreatedDate) : walks.OrderByDescending(x => x.CreatedDate); //ternary operator
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.CreatedDate) : walks.OrderByDescending(x => x.CreatedDate);
                }
            }

            //Paggination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();     // for paggination
                                                                                   //return await walks.ToListAsync();    
                                                                                   // return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
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
            existingProject.isActive = false;

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
