using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using ProjectDetailsAPI.Data;
using ProjectDetailsAPI.GenericRepo;
using ProjectDetailsAPI.Models.Domain;
using System.Linq.Expressions;
using System.Reflection;

namespace ProjectDetailsAPI.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly ProjectDetailsDbContext _dbContext;
        public DbSet<T> entities;   //for adding
        public GenericRepository(ProjectDetailsDbContext projectDetailsDbContext)
        {
            _dbContext = projectDetailsDbContext;
        }
        public void Add(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entities");
                }
                _dbContext.Set<T>().Add(entity);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) 
            {
                throw new Exception("Not able to add your data ", ex);
            }
        }       
        public IEnumerable<T> GetAll(int pageNumber = 1, int pageSize = 1000)
        {
            try
            {
                var skipResults = (pageNumber - 1) * pageSize;
                return _dbContext.Set<T>().Skip(skipResults).Take(pageSize).ToList();
            }     
            catch (Exception ex)
            {
                throw new Exception("Not able to find out the list of records ", ex);
            }
        }

        public T GetById(int id)
        {
            try
            {
                return _dbContext.Set<T>().Find(id);
            }
            catch (Exception ex) 
            {
                throw new Exception("Not able to find out this id ", ex);
            }
        }

        public void SoftDelete(T Entity)
        {
            try
            {
                PropertyInfo isDeletedProp = Entity.GetType().GetProperty("isDeleted");
                if (isDeletedProp != null)
                {
                    isDeletedProp.SetValue(Entity, true);
                    _dbContext.Set<T>().Update(Entity);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("This Entity does not have property named isDeleted");
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Not able to delete the data ", ex);
            }

        }

        public void Update(T Entity)
        {
            try
            {
                _dbContext.Set<T>().Update(Entity);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("not able to update data ", ex);
            }
        }
    }
}
