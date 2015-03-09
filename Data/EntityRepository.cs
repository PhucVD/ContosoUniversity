using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Data
{
    public class EntityRepository<TEntity> where TEntity: class
    {
        private DataContext context;
        private DbSet<TEntity> dbSet;

        public EntityRepository(DataContext dbContext)
        {
            this.context = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
            {
                return dbSet;
            }
            return dbSet.Where(expression);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            TEntity entity = dbSet.Find(id);
            Delete(entity);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
