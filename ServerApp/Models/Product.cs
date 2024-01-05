using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApp.Models
{
    [Table("product")]
    public class Product
    {
        [Column("product_code"),Key]
        public string ProductCode { get; set; } = "";
    }
}
