using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace TelegramDating.Database
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public virtual TEntity Get(long id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll() => Context.Set<TEntity>().ToList();

        public virtual void Add(TEntity entity) => Context.Set<TEntity>().Add(entity);

        public virtual void Remove(TEntity entity) => Context.Set<TEntity>().Remove(entity);

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            => Context.Set<TEntity>().Where(predicate);
    }
}
