using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TestApp.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetMany(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> orderBy,
            bool orderAsc = true);

        IEnumerable<TEntity> GetMany(
            Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetMany();

        TEntity GetSingle(Expression<Func<TEntity, bool>> filter);

        TEntity GetSingle();

        bool Exists(Expression<Func<TEntity, bool>> filter);

        void Insert(TEntity entity);

        void Update(TEntity updatedEntity);

        void Delete(TEntity entity);

        void Delete(Guid id);

        int Count();
    }
}
