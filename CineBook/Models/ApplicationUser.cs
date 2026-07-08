using Microsoft.AspNetCore.Identity;

namespace CineBook.Models
{
<<<<<<< HEAD
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
=======

    enum roles{
        admin,
        user
    
    }
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
>>>>>>> 8352284f62ad0f89281f6e0c9b349a6f36b7f0a5
