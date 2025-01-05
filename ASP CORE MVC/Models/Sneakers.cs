using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Microsoft.EntityFrameworkCore;

namespace ASP_CORE_MVC.Models
{
    [Table("Sneakers")]
    public class Sneakers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        /*public string ImageUrl { get; set; } // URL изображения*/

    }
}
