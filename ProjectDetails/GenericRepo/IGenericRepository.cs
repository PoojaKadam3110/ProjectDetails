using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjectDetailsAPI.GenericRepo
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll(int pageNumber = 1, int pageSize = 1000);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T Entity);
        void Update(T Entity);
        void AddRange(IEnumerable<T> entities);
        void SoftDelete(T Entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
