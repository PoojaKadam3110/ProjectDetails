namespace ProjectDetailsAPI.Services
{
    public interface IUnitOfWork : IDisposable
    {
        IClientGenRepository Clients { get; }
        IUsersRepository Users { get; }
        IProjectsRepository Projects { get; }
        IRoleRepository Roles { get; }

        int Save();
    }
}
