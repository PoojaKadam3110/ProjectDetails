using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;

namespace ProjectDetailsAPI.Implementation
{
    public class ClientGenRepository : GenericRepository<Clients> , IClientGenRepository
    {
        public ClientGenRepository(ProjectDetailsDbContext projectDetailsDbContext): base(projectDetailsDbContext)
        {
            
        }
    }
}