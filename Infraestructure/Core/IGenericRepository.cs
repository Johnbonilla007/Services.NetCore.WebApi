using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebServices.NetCore.Criostasis.Domain.Core;

namespace WebServices.NetCore.Criostasis
{
    public interface IGenericRepository<T> : IDisposable
       where T : IQueryableUnitOfWork

    {
        IUnitOfWork UnitOfWork { get; }

        void Add<TEntity>(TEntity entity)
            where TEntity : BaseEntity;

        void AddRange<TEntity>(IEnumerable<TEntity> entities)
           where TEntity : BaseEntity;

        void Remove<TEntity>(TEntity entity)
         where TEntity : BaseEntity;

        void RemoveRange<TEntity>(IEnumerable<TEntity> entities)
        where TEntity : BaseEntity;

        IEnumerable<TEntity> GetAll<TEntity>()
         where TEntity : BaseEntity;

        TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate)
        where TEntity : BaseEntity;
        IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate)
         where TEntity : BaseEntity;

        void Modify<TEntity>(TEntity item)
           where TEntity : BaseEntity;
    }
}