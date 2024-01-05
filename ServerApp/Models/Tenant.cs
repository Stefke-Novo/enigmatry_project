using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApp.Models
{
    [Table("tenant")]
    public class Tenant
    {
        [Column("tenant_id")]
        public string TenantId { get; set; } = "";

        [Column("account_number")]
        public int AccountNumber { get; set; } = 0;
    }
}
