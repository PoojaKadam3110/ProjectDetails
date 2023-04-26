using Microsoft.EntityFrameworkCore;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.Models.Domain;

namespace ProjectDetailsAPI.GenericRepo
{
    //public class Repository<T> : IRepo<T> where T : class
    public class Repository<T> : IRepo<T>  where T : class
    {
        private readonly ProjectDetailsDbContext _dbcontext;
        private readonly DbSet<T> entities;
        List<Clients> _clients;

        public Repository(ProjectDetailsDbContext dbcontext)
        {
            _dbcontext = dbcontext;
            entities = _dbcontext.Set<T>();
        }

        //public IEnumerable<T> GetAll()
        //{
        //    return entities.AsEnumerable();
        //}

        public T GetById(int Id)
        {
            return entities.Find(Id);           //.Where(x => x.isDeleted == false)
        }

        public List<Clients> GetAll()
        {
            return _clients;
        }

        List<T> IRepo<T>.GetAll()
        {
            return _dbcontext.Set<T>().ToList();
        }
    }
}
