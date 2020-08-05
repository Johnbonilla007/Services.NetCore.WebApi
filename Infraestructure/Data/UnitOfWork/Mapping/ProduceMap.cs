using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebServices.NetCore.Criostasis.Domain.Aggregates;

namespace WebServices.NetCore.Criostasis.Infraestructure.Data.UnitOfWork.Mapping
{
    public class ProduceMap
    {
        public ProduceMap(EntityTypeBuilder<Product> entityBuilder)
        {
            entityBuilder.HasKey(t => t.IdProducto);
            entityBuilder.Property(t => t.Quantity).IsRequired();
        }
    }
}