using Microsoft.AspNetCore.Identity;

namespace CineBook.Models
{

    enum roles{
        admin,
        user
    
    }
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}