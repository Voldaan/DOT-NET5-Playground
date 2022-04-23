using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Data
{
    public class CrudRepository<TEntity> : ICrud<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        public CrudRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            int saved = _dbContext.SaveChanges();
            return Convert.ToBoolean(saved);
        }

        public List<TEntity> Read()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public bool Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            int saved = _dbContext.SaveChanges();
            return Convert.ToBoolean(saved);
        }

        public bool Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            int saved = _dbContext.SaveChanges();
            return Convert.ToBoolean(saved);
        }
    }

}
