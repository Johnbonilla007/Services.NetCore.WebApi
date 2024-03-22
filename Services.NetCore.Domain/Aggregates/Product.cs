using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Services.NetCore.Crosscutting.Resources;
using Services.NetCore.Domain.Core;

namespace Services.NetCore.Domain.Aggregates
{
    [Table(nameof(Product), Schema = SchemaTypes.Commons)]
    public class Product : BaseEntity
    {
        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        public int Quantity { get; set; }

        public DateTime DatePurchase { get; set; }
    }
}