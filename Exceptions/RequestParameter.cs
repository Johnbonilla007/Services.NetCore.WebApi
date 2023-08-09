using System.ComponentModel.DataAnnotations.Schema;
using Services.NetCore.WebApi.AcationServices;
using WebServices.NetCore.Criostasis.Domain.Core;

namespace Services.NetCore.WebApi.Exceptions
{
    [Table(nameof(RequestParameter), Schema = SchemaTypes.ExceptionHandler)]
    public class RequestParameter : BaseEntity
    {
        public string Property { get; set; }
        public string Value { get; set; }

        public virtual LogExceptions LogException { get; set; }
    }
}
