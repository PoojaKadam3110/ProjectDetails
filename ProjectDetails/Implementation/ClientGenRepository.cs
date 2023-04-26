using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;

namespace ProjectDetailsAPI.Implementation
{
    public class ClientGenRepository : GenericRepository<Clients> , IClientGenRepository
    {
       // private readonly ProjectDetailsDbContext _dbcontext;
        public ClientGenRepository(ProjectDetailsDbContext projectDetailsDbContext): base(projectDetailsDbContext)
        {
            //_dbcontext = projectDetailsDbContext;   no need bcz inhereted from genericRepo
        }

        //public IEnumerable<Clients> GetClientsWithProjects()
        //{
        //    var clientsWithProjects = _dbContext.Clients.Include(x => x.ClientName).ToList();  //insted of clientName use another tbl name i.e projects
        //    return clientsWithProjects;
        //}
    }
}