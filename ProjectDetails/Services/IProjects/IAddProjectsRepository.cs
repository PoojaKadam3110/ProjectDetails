using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Models.DTO;

namespace ProjectDetailsAPI.Services.IProjects
{
    public interface IAddProjectsRepository
    {
        Task<List<Projects>> GetAll(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000); //modified thid method for filtering and middle two pata for sorting and last two for paggination
        Task<Projects> DeleteProjectById(int id);
        Task<Projects> AddProjects(ProjectsDto projects);
        Task<Projects> UpdateProject(int id, UpdateProjectsDto projects);

        int GetRecordCount();
        int GetDeletedRecordCount();
    }
}
