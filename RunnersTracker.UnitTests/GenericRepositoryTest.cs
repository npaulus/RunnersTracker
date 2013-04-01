using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using RunnersTrackerDB;

namespace RunnersTracker.DataAccess
{
    public class GenericRepositoryTest<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        //internal RunnersTrackerContext ctx;
        internal IQueryable<TEntity> dbSet;

        public GenericRepositoryTest(IQueryable<TEntity> ctx)
        {
            //this.ctx = ctx;
            this.dbSet = ctx;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.First();
        }

        public virtual void Insert(TEntity entity)
        {
            
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.First();
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            //dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate, TEntity originalEntity)
        {
            
        }
    }
}
