using Microsoft.EntityFrameworkCore;
using ServerApp.Config;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerApp.Models
{
    [EntityTypeConfiguration(typeof(TransactionConfiguration))]
    [Table("transaction")]
    public class Transaction
    {
        

        [Column("tenant_id"), JsonIgnore]
        public string TenantId { get; set; } = "";

        [Column("client_id"), JsonIgnore]
        public string ClientId { get; set; } = "";

        [Column("document_id"),JsonIgnore]
        public string DocumentId { get; set; } = "";

        [Column("transaction_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TransactionId { get; set; } = 0;

        [Column("amount")]
        public int Amount { get; set; } = 0;

        [Column("date")]
        public DateTime Date { get; set; } = new DateTime();

        [Column("description")]
        public string Description { get; set; } = "";

        [Column("category_id"), JsonIgnore]
        public int CategoryId { get; set; } = 0;
    }
}
