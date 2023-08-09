using System;
using System.ComponentModel.DataAnnotations;

namespace Services.NetCore.WebApi.Domain.Core
{
    public class Entity
    {
        [StringLength(50)]
        public string TransactionDescription { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
