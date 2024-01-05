
using Microsoft.EntityFrameworkCore;
using ServerApp.Config;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;

namespace ServerApp.Models
{
    [EntityTypeConfiguration(typeof(ClientConfiguration))]
    [Table("client")]
    public class Client
    {
        [Column("client_id")]
        public string ClientId { get; set; } = "";

        [Column("client_vat")]
        public string ClientVAT { get; set; } = "";

        [Column("registration_number")]
        public int RegistrationNumber { get; set; } = 0;

        [Column("balance")]
        public int Balance { get; set; } = 0;

        [Column("currency_id")]
        public int CurrencyId { get; set; } = 0;

        [Column("company_type")]
        public string CompanyType { get; set; } = "";
        [JsonIgnore]
        public Currency Currency { get; set; } = new Currency();

        [JsonIgnore,NotMapped]
        public ICollection<Document> Documents { get; set; } = new Collection<Document>();

        public override bool Equals(object? obj)
        {
            return obj is Client client &&
                   ClientId == client.ClientId &&
                   ClientVAT == client.ClientVAT;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ClientId, ClientVAT);
        }
    }
}
