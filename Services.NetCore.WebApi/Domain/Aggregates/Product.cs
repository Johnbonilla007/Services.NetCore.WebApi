using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebServices.NetCore.Criostasis.Domain.Core;

namespace WebServices.NetCore.Criostasis.Domain.Aggregates
{
    [Table("Producto")]
    public class Product : BaseEntity
    {
        [Key]
        public int IdProducto { get; set; }
        [Column("PrecioCompra")]
        public decimal PurchasePrice { get; set; }
        [Column("CantidadProducto")]
        public int Quantity { get; set; }
        [Column("FechaCompra")]
        public DateTime DatePurchase { get; set; }
    }
}