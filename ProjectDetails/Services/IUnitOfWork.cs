using ProjectDetailsAPI.Services.IProjects;

namespace ProjectDetailsAPI.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IClientGenRepository Clients { get; }
        IUsersRepository Users { get; }
        IProjectTestRepository Projects { get; }

        int Save();
    }
}
