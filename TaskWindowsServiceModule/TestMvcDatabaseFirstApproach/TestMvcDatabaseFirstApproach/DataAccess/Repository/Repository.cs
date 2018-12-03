using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace TestMvcDatabaseFirstApproach.DataAccess.Repository
{
    public class Repository<TEntity> where TEntity: class
    {
        internal readonly FakePersonDatabaseEntities _dbContext;
        internal readonly DbSet<TEntity> _dbSet;

        public Repository()
        {
            _dbContext = new FakePersonDatabaseEntities();
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            foreach(var includeProperty in includeProperties.Split
                (new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            var result = _dbSet.Add(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            var result = _dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(int id)
        {
            var entityToDelete = _dbSet.Find(id);
            var result = _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = System.Data.EntityState.Modified;
        }
    }
}