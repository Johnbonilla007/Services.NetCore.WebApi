using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebServices.NetCore.Criostasis.Domain.Aggregates;
using WebServices.NetCore.Criostasis.Infraestructure.Data.UnitOfWork.Mapping;

namespace WebServices.NetCore.Criostasis.Infraestructure.Data.UnitOfWork
{
    public class Context : DbContext, IQueryableUnitOfWork
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Produce>();

            new ProduceMap(modelBuilder.Entity<Product>());
        }

        void IQueryableUnitOfWork.ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
        {
            //if it is not attached, attach original and set current values
            Entry(original).CurrentValues.SetValues(current);
        }

        void IQueryableUnitOfWork.Attach<TEntity>(TEntity item)
        {
            //attach and set as unchanged
            Entry(item).State = EntityState.Unchanged;
        }

        void IUnitOfWork.Commit()
        {
            base.SaveChanges();
        }

        void IUnitOfWork.CommitAndRefreshChanges()
        {
            bool saveFailed;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
                }
            } while (saveFailed);
        }

        DbSet<TEntity> IQueryableUnitOfWork.CreateSet<TEntity>()
        {
            return Set<TEntity>();
        }

        void IQueryableUnitOfWork.SetModified<TEntity>(TEntity item)
        {
            throw new System.NotImplementedException();
        }
    }

}