using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Data;
using Dapper;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;

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
    }
}
