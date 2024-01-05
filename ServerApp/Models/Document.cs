using Microsoft.EntityFrameworkCore;
using ServerApp.Config;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerApp.Models
{
    [EntityTypeConfiguration(typeof(DocumentConfiguration))]
    [Table("document")]
    public class Document
    {
        [Column("tenant_id")]
        [ForeignKey(nameof(DocumentTenant))]
        public string TenantId { get; set; } = "";

        [Column("client_id")]
        [ForeignKey(nameof(DocumentClient))]
        public string ClientId { get; set; } = "";

        [Column("document_id")]
        public string DocumentId { get; set; } = "";

        [Column("currency")]
        public string Currency { get; set; } = "";

        [JsonIgnore, NotMapped]
        public Tenant DocumentTenant { get; set; } = new Tenant();

        [JsonIgnore, NotMapped]
        public Client DocumentClient { get; set; } = new Client();
        [JsonIgnore, NotMapped]
        public ICollection<Transaction> Transations { get; set; } = new Collection<Transaction>();
    }
}
