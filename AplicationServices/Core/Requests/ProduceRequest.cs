using System;

namespace WebServices.NetCore.Criostasis.AplicationServices.Core.Requests
{
    public class ProduceRequest : RequestBase
    {
        public int IdProducto { get; set; }

        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public DateTime DatePurchase { get; set; }
    }
}