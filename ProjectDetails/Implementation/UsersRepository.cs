using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;

namespace ProjectDetailsAPI.Implementation
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        private readonly ProjectDetailsDbContext _dbcontext;
        public UsersRepository(ProjectDetailsDbContext projectDetailsDbContext): base(projectDetailsDbContext)
        {
            _dbcontext = projectDetailsDbContext;
        }
    }
}
