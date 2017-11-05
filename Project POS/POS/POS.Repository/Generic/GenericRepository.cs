using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using POS.Context;

namespace POS.Repository.Generic
{
    public class GenericRepository<TEntity> where TEntity: class
    {
        internal AsowellContext context;
        internal DbSet dbSet;

        public GenericRepository(AsowellContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }


        /// <summary>
        /// Get data
        /// </summary>
        /// <param name="filter">Lambda expression to filtering data</param>
        /// <param name="orderBy">Lambda expression to ordering data</param>
        /// <param name="includeProperties">the properties represent the relationship with other entities (use ',' to seperate these properties)</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            // Apply the filter expression
            IQueryable<TEntity> query = (IQueryable<TEntity>) dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Loading related data (using eager-loading)
            foreach (var includeProperty in includeProperties.Split(
                new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            // Apply the orderBy expression
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return (TEntity) dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = (TEntity) dbSet.Find(id);
            dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(TEntity entityTODelete)
        {
            if (context.Entry(entityTODelete).State == EntityState.Deleted)
            {
                dbSet.Attach(entityTODelete);
            }
            dbSet.Remove(entityTODelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        } 
    }
}
