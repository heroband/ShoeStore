using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Models.ViewModels
{
    public class SneakersViewModel
    {
        [Required(ErrorMessage = "You need to input name")]
        [Display(Name = "Input sneakers name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "You need to input description")]
        [Display(Name = "Input sneakers description")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "You need to input size")]
        [Display(Name = "Input sneakers size")]
        [Range(1, 99, ErrorMessage = "Size must be between 1 and 99.")]
        public int Size { get; set; }
        [Required(ErrorMessage = "You need to input price")]
        [Display(Name = "Input sneakers price")]
        public decimal Price { get; set; }
        [Url(ErrorMessage = "Invalid URL format.")]
        [Display(Name = "Input sneakers image URL")]
        public string ImageUrl { get; set; }
    }
}
