using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjectDetailsAPI.GenericRepo
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll(int pageNumber = 1, int pageSize = 1000);
        void Add(T Entity);
        void Update(T Entity);
        void SoftDelete(T Entity);
    }
}
