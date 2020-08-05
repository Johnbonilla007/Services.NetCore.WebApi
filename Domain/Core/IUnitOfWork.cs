using System;

namespace WebServices.NetCore.Criostasis
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void CommitAndRefreshChanges();
    }
}