using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Ocsp;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Models.DTO;
using ProjectDetailsAPI.Services;
using ProjectDetailsAPI.Services.IProjects;

namespace ProjectDetailsAPI.Implementation.ProjectsImp
{
    public class ProjectsRepository :  IProjectsRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ProjectDetailsDbContext _dbcontext;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectsRepository(IConfiguration configuration,ProjectDetailsDbContext dbcontext,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Projects>> GetAll(string? filterOn = null, string? filterQuery = null,
          string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)  //modified thid method for filtering and middle two for sorting and last two for pagginatiom
        {
            try
            {
                var data = _dbcontext.Projects.AsQueryable();
                //Filterring
                if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
                {
                    if (filterOn.Equals("Project Name", StringComparison.OrdinalIgnoreCase))
                    {
                        data = data.Where(x => x.ProjectName.Contains(filterQuery));
                    }
                }

                //sorting
                if (string.IsNullOrWhiteSpace(sortBy) == false)
                {
                    if (sortBy.Equals("Date", StringComparison.OrdinalIgnoreCase))
                    {
                        data = isAscending ? data.OrderBy(x => x.CreatedDate) : data.OrderByDescending(x => x.CreatedDate); //ternary operator
                    }
                    else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                    {
                        data = isAscending ? data.OrderBy(x => x.CreatedDate) : data.OrderByDescending(x => x.CreatedDate);
                    }
                }

                //Paggination
                var skipResults = (pageNumber - 1) * pageSize;

                return await data.Skip(skipResults).Take(pageSize).ToListAsync();     // for paggination
                                                                                      //return await walks.ToListAsync();    

            }
            catch (Exception ex)
            {
                throw new Exception("You are not able to see the project list");
            }
        }



        public async Task<Projects> AddProjects(ProjectsDto projects)
        {
            try
            {
                Projects projectsDomain = new Projects();
                projectsDomain.ProjectName = projects.ProjectName;
                projectsDomain.ClientName = projects.ClientName;
                projectsDomain.projectManager = projects.projectManager;
                projectsDomain.ratePerHour = projects.ratePerHour;
                projectsDomain.projectCost = projects.projectCost;
                projectsDomain.projectUsers = projects.projectUsers;
                projectsDomain.description = projects.description;
                projectsDomain.isActive = true;
                projectsDomain.isDeleted = false;
                projectsDomain.CreatedBy = "Pooja";
                projectsDomain.CreatedDate = DateTime.Now;
                projectsDomain.UpdatedBy = "Pooja";
                projectsDomain.UpdatedDate = DateTime.Now;

                _unitOfWork.Projects.Add(projectsDomain);

                return projectsDomain;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred to save the project");
            }
        }

        public async Task<Projects> DeleteProjectById(int id)
        {
            try
            {
                var existingProject = await _dbcontext.Projects.FirstOrDefaultAsync(x => x.Id == id);
                if (existingProject == null)
                {
                    return null;
                }

                if (existingProject.isDeleted == true)
                {
                    return null;
                }               

                existingProject.isDeleted = true;
                existingProject.isActive = false;

                await _dbcontext.SaveChangesAsync();

                return existingProject;
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to delete project ",ex);
            }
        }

        public async Task<Projects> UpdateProject(int id, UpdateProjectsDto projects)
        {
            try
            {
                var existingProject = await _dbcontext.Projects.FirstOrDefaultAsync(x => x.Id == id);

                //if (existingProject == null)
                //{
                //    return null;
                //}
                existingProject.ProjectName = projects.ProjectName;
                existingProject.projectManager = projects.projectManager;
                existingProject.projectCost = projects.projectCost;
                existingProject.ratePerHour = projects.ratePerHour;
                existingProject.description = projects.description;
                existingProject.ClientName = projects.ClientName;
                existingProject.UpdatedDate = DateTime.Now;

                await _dbcontext.SaveChangesAsync();

                return existingProject;
            }
            catch (Exception ex) 
            {
                throw new Exception("your not able to update project please entered some data first", ex);
            }
        }

        public  int GetRecordCount()
        {
            try
            {
                int count = _dbcontext.Projects.Where(x => x.isDeleted == false).Count();
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception("you are not ablle to see the active projects ",ex);
            }
        }
        public  int GetDeletedRecordCount()
        {
            try
            {
                int count = _dbcontext.Projects.Where(x => x.isDeleted == true).Count();
                return count;
            }
            catch(Exception ex) 
            {
                throw new Exception("you are not ablle to see the Deleted projects ",ex);
            }
        }
    }
}
