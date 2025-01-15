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
        [Required(ErrorMessage = "You need to input at least one size")]
        [Display(Name = "Input sneakers sizes (e.g., '36 37 38')")]
        [RegularExpression(@"^(\d{2}\s?)+$", ErrorMessage = "Sizes must be two-digit numbers separated by spaces.")]
        public string Sizes { get; set; }
        [Required(ErrorMessage = "You need to input price")]
        [Display(Name = "Input sneakers price")]
        public decimal Price { get; set; }
        [Url(ErrorMessage = "Invalid URL format.")]
        [Display(Name = "Input sneakers image URL")]
        public string ImageUrl { get; set; }
    }
}
