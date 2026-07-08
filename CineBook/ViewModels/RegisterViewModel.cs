using System.ComponentModel.DataAnnotations;

namespace CineBook.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
<<<<<<< HEAD
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
=======
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
<<<<<<< HEAD
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
=======
        public string ConfirmPassword { get; set; }
    }
}
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5
