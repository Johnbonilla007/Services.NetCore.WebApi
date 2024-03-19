using System;
using System.Threading.Tasks;
using Services.NetCore.WebApi.Domain.Core;

namespace WebServices.NetCore.Criostasis
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        void CommitAndRefreshChanges();
        Task CommitAsync(TransactionInfo transactionInfo);
    }
}