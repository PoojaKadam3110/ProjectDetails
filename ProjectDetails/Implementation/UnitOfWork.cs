using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Implementation.ProjectsImp;
using ProjectDetailsAPI.Models.Domain;
using ProjectDetailsAPI.Services;
using ProjectDetailsAPI.Services.IProjects;

namespace ProjectDetailsAPI.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDetailsDbContext _dbcontext;
        public UnitOfWork(ProjectDetailsDbContext context)
        {
            _dbcontext = context;
            Clients = new ClientGenRepository(_dbcontext);
            Users = new UsersRepository(_dbcontext);
            Projects = new ProjectTestRepository(_dbcontext);
            Roles = new RoleRepository(_dbcontext);
        }

        public IClientGenRepository Clients { get; private set; }
        public IUsersRepository Users { get; private set; }
        public IProjectTestRepository Projects { get; private set; }
        public IRoleRepository Roles { get; private set; }

        public int Save()
        {
            return _dbcontext.SaveChanges();
        }
        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
