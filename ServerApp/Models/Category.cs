using System.ComponentModel.DataAnnotations.Schema;

namespace ServerApp.Models
{
    [Table("category")]
    public class Category
    {
        [Column(name:"category_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CategoryId { get; set; } = 0;

        [Column("name")]
        public string Name { get; set; } = "";
    }
}
