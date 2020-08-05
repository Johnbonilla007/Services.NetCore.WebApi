using System;
using System.Collections.Generic;
using System.Linq;
using WebServices.NetCore.Criostasis.AplicationServices.Core.DTOs;
using WebServices.NetCore.Criostasis.AplicationServices.Core.Requests;
using WebServices.NetCore.Criostasis.Domain.Aggregates;


namespace WebServices.NetCore.Criostasis.AplicationServices.Produce
{
    public class ProduceAppService : IProduceAppService
    {
        private readonly IGenericRepository<IGenericDataContext> _repository;
        public ProduceAppService(IGenericRepository<IGenericDataContext> repository)
        {
            if (repository == null) throw new ArgumentException(nameof(repository));

            _repository = repository;

        }
        public List<ProduceDto> GetProducts(ProduceRequest request)
        {
            var products = _repository.GetAll<Product>().ToList();

            return new List<ProduceDto>();
        }

        public ProduceDto SaveProduce(ProduceRequest request)
        {
            Product product = new Product
            {
                PurchasePrice = 11,
                Quantity = 12,
                DatePurchase = DateTime.Now
            };
            List<Product> products = new List<Product>();

            products.Add(product);

            _repository.AddRange(products);
            _repository.UnitOfWork.Commit();

            return new ProduceDto { Message = "Success" };
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}