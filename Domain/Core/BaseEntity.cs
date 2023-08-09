using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServices.NetCore.Criostasis.Domain.Core
{
    public class BaseEntity
    {
        [NotMapped]
        public const int longVarcharLength = 200;
        [NotMapped]
        public const int shortVarcharLength = 50;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid TransactionUId { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDateUtc { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ModifiedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; } = true;

        [StringLength(50)]
        public string TransactionDescription { get; set; }

        public BaseEntity()
        {
        }

        public BaseEntity(string modifiedBy, string transactionType)
        {
            ModifiedBy = modifiedBy;
            TransactionType = transactionType;
            TransactionDateUtc = DateTime.UtcNow;
            TransactionUId = Guid.NewGuid();
        }
    }
}