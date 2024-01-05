using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerApp.Models
{
    [Table("currency")]
    public class Currency
    {
        [Column("currency_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CurrencyId { get; set; } = 0;

        [Column("currency_name")]
        public string CurrencyName { get; set; } = "";

        [JsonIgnore,NotMapped]
        public List<Client> Clients { get; set; } = new List<Client>();
    }
}
