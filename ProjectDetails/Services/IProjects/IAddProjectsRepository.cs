using ProjectDetailsAPI.Models.Domain;

namespace ProjectDetailsAPI.Services.IProjects
{
    public interface IAddProjectsRepository
    {
        Task<Projects> DeleteProjectById(int id);
        Task<Projects> AddProjects(Projects projects);
        Task<Projects> UpdateProject(int id, Projects projects);
    }
}
