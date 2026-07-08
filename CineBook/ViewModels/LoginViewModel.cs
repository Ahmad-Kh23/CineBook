using System.ComponentModel.DataAnnotations;

namespace CineBook.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
<<<<<<< HEAD
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
=======
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5
