using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ASP_CORE_MVC.Models
{
    public class Sneakers
    {
        [Display(Name = "Input sneakers name")]
        [Required(ErrorMessage = "You need to input name")]
        public string? Name { get; set; }
        [Display(Name = "Input sneakers description")]
        [Required(ErrorMessage = "You need to input description")]
        public string? Description { get; set; }
        [Display(Name = "Input sneakers id")]
        [Required(ErrorMessage = "You need to input id")]
        public string Id { get; set; }
        [Display(Name = "Input sneakers size")]
        [Required(ErrorMessage = "You need to input size")]
        [Range(1, 99, ErrorMessage = "Size must be between 1 and 99.")]
        public int Size { get; set; }
        [Display(Name = "Input sneakers price")]
        [Required(ErrorMessage = "You need to input price")]
        public decimal Price { get; set; }

    }
}
