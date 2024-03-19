using System;

namespace WebServices.NetCore.Criostasis.AplicationServices.Core.DTOs
{
    public class ProduceDto : ResponseBase
    {
        public int IdProducto { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public DateTime DatePurchase { get; set; }

    }
}