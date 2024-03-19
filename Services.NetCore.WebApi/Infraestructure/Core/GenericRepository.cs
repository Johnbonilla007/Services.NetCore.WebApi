using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.NetCore.WebApi.Domain.Core;
using WebServices.NetCore.Criostasis.Domain.Core;

namespace WebServices.NetCore.Criostasis
{
    public class GenericRepository<T> : IGenericRepository<T>
          where T : IQueryableUnitOfWork
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public GenericRepository(IQueryableUnitOfWork UnitOfWork, IConfiguration configuration)
        {
            _unitOfWork = UnitOfWork;
            _configuration = configuration;
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
                entity.CreationDate = DateTime.Now;

                GetSet<TEntity>().Add(entity);
            }
        }
        public void Add<TEntity>(TEntity entity, TransactionInfo transactionInfo) where TEntity : BaseEntity
        {
            if (entity != null)
            {
                entity.ModifiedBy = transactionInfo.ModifiedBy;
                entity.TransactionType = transactionInfo.TransactionType;
                entity.CreationDate = transactionInfo.CreationDate;
                entity.IsActive = true;
                entity.TransactionDateUtc = transactionInfo.TransactionDateUtc;
                entity.TransactionDate = DateTime.Now;
                entity.RowVersion = Array.Empty<byte>();
                entity.TransactionUId = transactionInfo.TransactionUId;
                entity.TransactionDescription = Transactions.Insert;

                GetSet<TEntity>().Add(entity);
            }
        }
        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity != null)
            {
                entity.CreationDate = DateTime.Now;

                await GetSet<TEntity>().AddAsync(entity);
            }
        }
        public async Task AddAsync<TEntity>(TEntity entity, TransactionInfo transactionInfo) where TEntity : BaseEntity
        {
            if (entity != null)
            {
                entity.ModifiedBy = transactionInfo.ModifiedBy;
                entity.TransactionType = transactionInfo.TransactionType;
                entity.CreationDate = DateTime.Now;
                entity.IsActive = true;
                entity.TransactionDateUtc = transactionInfo.TransactionDateUtc;
                entity.TransactionDate = DateTime.Now;
                entity.RowVersion = Array.Empty<byte>();
                entity.TransactionUId = transactionInfo.TransactionUId;
                entity.TransactionDescription = Transactions.Insert;

                await GetSet<TEntity>().AddAsync(entity);

                await _unitOfWork.CommitAsync(transactionInfo);
            }
        }
        public async Task<TEntity> AddAndGetIdAsync<TEntity>(TEntity entity, TransactionInfo transactionInfo) where TEntity : BaseEntity
        {
            if (entity != null)
            {
                var addedEntity = GetSet<TEntity>().Add(entity).Entity;
                await UnitOfWork.CommitAsync(transactionInfo);
                return addedEntity;

            }

            return null;
        }
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            if (entities != null)
            {
                GetSet<TEntity>().AddRange(entities);
            }
        }
        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            if (entities != null)
            {
                await GetSet<TEntity>().AddRangeAsync(entities);
            }
        }
        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : BaseEntity
        {
            return GetSet<TEntity>().ToList();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity
        {
            return await GetSet<TEntity>().ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes, Expression<Func<TEntity, object>> includeFilter = null) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                items = includes.Aggregate(items, (current, include) =>
                {
                    return current.Include(include);
                });
            }

            if (includeFilter != null)
            {
                items = items.Include(includeFilter);
            }
            return await items.Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();
            if (includes != null && includes.Any())
            {
                items = includes.Aggregate(items, (current, include) =>
                {
                    return current.Include(include);

                });

            }
            return await items.Where(predicate).ToListAsync();

        }
        public async Task<IEnumerable<TEntity>> GetAllIncludeAsync<TEntity>(List<string> includes) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                items = includes.Aggregate(items, (current, include) =>
                {
                    return current.Include(include);
                });
            }

            return await items.ToListAsync();

        }
        public async Task<TEntity> GetIncludeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                items = includes.Aggregate(items, (current, include) =>
                {
                    return current.Include(include);
                });
            }

            return await items.FirstOrDefaultAsync(predicate);
        }
        public async Task<TEntity> GetIncludeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes, Expression<Func<TEntity, object>> includeFilter = null) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                items = includes.Aggregate(items, (current, include) =>
                {
                    return current.Include(include);
                });
            }
            if (includeFilter != null)
            {
                items = items.Include(includeFilter);
            }

            return await items.FirstOrDefaultAsync(predicate);
        }
        public IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return GetSet<TEntity>().Where(predicate).ToList();
        }
        public async Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return await GetSet<TEntity>().Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();



            if (includes != null && includes.Any())
            {
                items = includes.Aggregate(items, (current, include) =>
                {
                    return current.Include(include);
                });
            }



            return await items.Where(predicate).ToListAsync();
        }
        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            return items.FirstOrDefault(predicate);
        }
        public async Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                items = includes.Aggregate(items, (current, include) =>
                {
                    return current.Include(include);
                });
            }

            return await items.FirstOrDefaultAsync(predicate);
        }
        public async Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            return await items.FirstOrDefaultAsync(predicate);
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
        public async Task RemoveAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            TEntity entity = await GetSet<TEntity>().FirstOrDefaultAsync(predicate);

            if (entity != null)
            {
                //attach item if not exist
                _unitOfWork.Attach(entity);

                //set as "removed"
                GetSet<TEntity>().Remove(entity);
            }
            else
            {

            }
        }
        public async Task RemoveAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
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

            }
        }
        public Task RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            if (entities != null)
            {
                GetSet<TEntity>().RemoveRange(entities);
            }

            return Task.CompletedTask;
        }
        public Task RemoveRangeAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>().Where(predicate);

            if (items != null && items.Any())
            {
                //attach item if not exist
                GetSet<TEntity>().AttachRange(items);

                //set as "removed"
                GetSet<TEntity>().RemoveRange(items);


                return Task.CompletedTask;
            }
            else
            {
                return Task.CompletedTask;
            }
        }
        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }
        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity != null)
            {
                entity.TransactionDate = DateTime.Now;
                entity.TransactionDescription = Transactions.Update;

                GetSet<TEntity>().Update(entity);
            }
        }
        public async Task<IEnumerable<T>> ExecuteSqlCommandAsync<T>(SqlCommand command, object parameters)
        {
            if (command.Connection == null)
            {
                var conectecion = new SqlConnection(_configuration.GetConnectionString("ConectionString"));
                command.Connection = conectecion;
            }

            if (command.Connection.State == ConnectionState.Closed)
            {
                await command.Connection.OpenAsync();
            }

            using (var cmd = new SqlCommand())
            {
                cmd.Connection = command.Connection;
                cmd.CommandText = command.CommandText;
                cmd.CommandType = command.CommandType;
                cmd.CommandTimeout = command.CommandTimeout;

                if (parameters != null)
                {
                    var props = parameters.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    foreach (var prop in props)
                    {
                        var parameter = new SqlParameter($"@{prop.Name}", prop.GetValue(parameters));
                        cmd.Parameters.Add(parameter);
                    }
                }

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var result = new List<T>();

                    while (await reader.ReadAsync())
                    {
                        var obj = Activator.CreateInstance<T>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var property = typeof(T).GetProperty(reader.GetName(i), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                            if (property != null && !reader.IsDBNull(i))
                            {
                                var value = reader.GetValue(i);
                                property.SetValue(obj, value);
                            }
                        }

                        result.Add(obj);
                    }

                    return result;
                }
            }
        }
        public async Task<IEnumerable<T>> ExecuteSqlCommandAsync<T>(SqlCommand command, object parameters, string externalConectionString)
        {
            if (command.Connection == null)
            {
                command.Connection = new SqlConnection(externalConectionString);
            }

            if (command.Connection.State == ConnectionState.Closed)
            {
                await command.Connection.OpenAsync();
            }

            using (var cmd = new SqlCommand())
            {
                cmd.Connection = command.Connection;
                cmd.CommandText = command.CommandText;
                cmd.CommandType = command.CommandType;
                cmd.CommandTimeout = command.CommandTimeout;

                if (parameters != null)
                {
                    var props = parameters.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                    foreach (var prop in props)
                    {
                        var parameter = new SqlParameter($"@{prop.Name}", prop.GetValue(parameters));
                        cmd.Parameters.Add(parameter);
                    }
                }

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    var result = new List<T>();

                    while (await reader.ReadAsync())
                    {
                        var obj = Activator.CreateInstance<T>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var property = typeof(T).GetProperty(reader.GetName(i), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                            if (property != null && !reader.IsDBNull(i))
                            {
                                var value = reader.GetValue(i);
                                property.SetValue(obj, value);
                            }
                        }

                        result.Add(obj);
                    }

                    return result;
                }
            }
        }
        public IQueryable<TEntity> GetAllWithoutFilters<TEntity>() where TEntity : BaseEntity
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            return items;
        }
    }
}