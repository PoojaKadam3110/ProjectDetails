using Microsoft.AspNetCore.Mvc;
using ProjectDetailsAPI.Models.Domain;

namespace ProjectDetailsAPI.Data.Command
{
    public class AddClientCommand
    {
        private readonly ProjectDetailsDbContext _dbcontext;
        public AddClientCommand(ProjectDetailsDbContext dbContext) 
        { 
            _dbcontext = dbContext;
        }

        public async Task<Clients> Insert(Clients clients)
        {
            await _dbcontext.Clients.AddAsync(clients);
            await _dbcontext.SaveChangesAsync();

            return clients;
        }
    }
    
}
