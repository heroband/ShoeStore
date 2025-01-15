using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStore.Models.Entities
{
    [Table("Sneakers")]
    public class Sneakers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Sizes { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
