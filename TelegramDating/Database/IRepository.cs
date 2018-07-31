using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TelegramDating.Database
{
    interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(long id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Remove(TEntity entity);
        
        // void Remove(string username);

        
    }
}
