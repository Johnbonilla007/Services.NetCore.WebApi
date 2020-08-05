using System;
using System.Collections.Generic;
using WebServices.NetCore.Criostasis.AplicationServices.Core.DTOs;
using WebServices.NetCore.Criostasis.AplicationServices.Core.Requests;

namespace WebServices.NetCore.Criostasis.AplicationServices.Produce
{
    public interface IProduceAppService : IDisposable
    {
        ProduceDto SaveProduce(ProduceRequest request);
        List<ProduceDto> GetProducts(ProduceRequest request);
    }
}