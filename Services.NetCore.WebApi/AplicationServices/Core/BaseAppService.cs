using System;
using WebServices.NetCore.Criostasis;

namespace Services.NetCore.WebApi.AplicationServices
{
    public class BaseAppService : IDisposable
    {
        protected readonly IGenericRepository<IGenericDataContext> _repository;

        public BaseAppService(IGenericRepository<IGenericDataContext> repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
