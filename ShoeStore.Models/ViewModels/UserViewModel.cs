using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Models.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Enter username")]
        [MaxLength(20, ErrorMessage = "Username cannot exceed 20 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Enter your password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your password.")]
        [Display(Name = "Confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
