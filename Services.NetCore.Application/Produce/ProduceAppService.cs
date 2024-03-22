using Services.NetCore.Application.Core;
using Services.NetCore.Crosscutting.Dtos.Produce;
using Services.NetCore.Domain.Aggregates;
using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork;

namespace Services.NetCore.Application.Produce
{
    public class ProduceAppService : BaseAppService, IProduceAppService
    {
        public ProduceAppService(IGenericRepository<IGenericDataContext> repository) : base(repository)
        {
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