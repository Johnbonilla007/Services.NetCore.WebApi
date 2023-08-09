using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Services.NetCore.WebApi.AcationServices;
using WebServices.NetCore.Criostasis.Domain.Core;

namespace Services.NetCore.WebApi.Exceptions
{
    [Table(nameof(LogExceptions), Schema = SchemaTypes.ExceptionHandler)]
    public class LogExceptions : BaseEntity
    {
        public string Message { get; set; }
        public string ExceptionError { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string HttpMethod { get; set; }

        public virtual ICollection<RequestParameter> RequestParameters { get; set; }
    }
}
