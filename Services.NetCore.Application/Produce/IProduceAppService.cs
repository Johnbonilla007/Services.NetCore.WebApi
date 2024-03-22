using Services.NetCore.Crosscutting.Dtos.Produce;

namespace Services.NetCore.Application.Produce
{
    public interface IProduceAppService : IDisposable
    {
        ProduceDto SaveProduce(ProduceRequest request);
        List<ProduceDto> GetProducts(ProduceRequest request);
    }
}