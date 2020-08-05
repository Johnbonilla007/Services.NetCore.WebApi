using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebServices.NetCore.Criostasis.Domain.Core;

namespace WebServices.NetCore.Criostasis
{
    public class GenericRepository<T> : IGenericRepository<T>
          where T : IQueryableUnitOfWork
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public GenericRepository(IQueryableUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }
        private DbSet<TEntity> GetSet<TEntity>() where TEntity : BaseEntity
        {
            DbSet<TEntity> set = _unitOfWork.CreateSet<TEntity>();

            return set;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity != null)
            {
                GetSet<TEntity>().Add(entity);
            }
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            if (entities != null)
            {
                GetSet<TEntity>().AddRange(entities);
            }
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : BaseEntity
        {
            return GetSet<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return GetSet<TEntity>().Where(predicate).ToList();
        }

        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            return items.FirstOrDefault(predicate);
        }

        public void Modify<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            if (item != null)
                _unitOfWork.SetModified(item);
            else
            {
                //LoggerFactory.CreateLog().LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity != null)
            {
                //attach item if not exist
                _unitOfWork.Attach(entity);

                //set as "removed"
                GetSet<TEntity>().Remove(entity);
            }
            else
            {
                //LoggerFactory.CreateLog().LogInfo("Cannot remove null entity.", typeof(TEntity).ToString());
            }
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            if (entities != null)
            {
                GetSet<TEntity>().RemoveRange(entities);
            }
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }
    }
}