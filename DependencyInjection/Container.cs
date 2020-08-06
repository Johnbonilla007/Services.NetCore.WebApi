using System;
using Microsoft.Extensions.DependencyInjection;
using WebServices.NetCore.Criostasis;
using WebServices.NetCore.Criostasis.AplicationServices.Produce;
using WebServices.NetCore.Criostasis.Infraestructure.Data.UnitOfWork;

namespace Services.NetCore.WebApi.DependencyInjection
{
    public class Container
    {
        private readonly IServiceCollection _services;
        public Container(IServiceCollection services)
        {
            if (services == null) throw new ArgumentException(nameof(services));

            _services = services;

            InitializeUnitsOfWork();
            InitializeApplicationServices();
            InitializeDomainServices();
            InitializeRepositories();
        }

        private void InitializeRepositories()
        {
            _services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        private void InitializeDomainServices()
        {

        }

        private void InitializeApplicationServices()
        {

            _services.AddScoped(typeof(IProduceAppService), typeof(ProduceAppService));

        }

        private void InitializeUnitsOfWork()
        {
            _services.AddScoped(typeof(IQueryableUnitOfWork), typeof(Context));
        }
    }
}