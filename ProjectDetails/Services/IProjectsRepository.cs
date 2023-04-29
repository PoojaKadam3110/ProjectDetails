using ProjectDetailsAPI.GenericRepo;
using ProjectDetailsAPI.Models.Domain;

namespace ProjectDetailsAPI.Services
{
    public interface IProjectsRepository : IGenericRepository<Clients>
    {
    }
}
