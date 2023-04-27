using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Data;
using Dapper;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using ProjectDetailsAPI.Models.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectDetailsAPI.Services
{
    public class ClientRepository : IClientRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ProjectDetailsDbContext _dbcontext;
        public ClientRepository(IConfiguration configuration, ProjectDetailsDbContext dbcontext)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
        }

        public async Task<Clients> AddClients(Clients clients)
        {
            await _dbcontext.Clients.AddAsync(clients);
            await _dbcontext.SaveChangesAsync();
            return clients;
           // return clients;
        }

        public async Task<Clients> DeleteClientById(int id)
        {
            var existingClient = await _dbcontext.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if (existingClient == null)
            {
                return null;
            }

            existingClient.isDeleted = true;

            await _dbcontext.SaveChangesAsync();

            return existingClient;
        }

        public async Task<List<ClientResponse>> GetUsers()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("ProjectDetailsConnectionStrings")))
            {
                var query = $@"
                        SELECT 
                             c.Id
                            ,c.ClientName
                            ,c.IsActive
                            ,c.IsDeleted
                            ,c.CreatedDate
                            ,c.CreatedBy
                            ,c.UpdatedDate
                            ,c.UpdatedBy
                           
                        FROM Clients c
                        WHERE c.IsDeleted = 0
                        ORDER BY c.ClientName";

                return (await connection.QueryAsync<ClientResponse>(query).ConfigureAwait(false)).ToList();
            }
        }

        public async Task<Clients> UpdateClients(int id, Clients clients)
        {

            var existingClient = await _dbcontext.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if (existingClient == null)
            {
                return null;
            }
            existingClient.ClientName = clients.ClientName;
            existingClient.UpdatedDate = clients.UpdatedDate;
            existingClient.isDeleted = clients.isDeleted;
            existingClient.CreatedBy = clients.CreatedBy;
            existingClient.UpdatedBy = clients.UpdatedBy;

            await _dbcontext.SaveChangesAsync();

            return existingClient;
        }
    }
}
