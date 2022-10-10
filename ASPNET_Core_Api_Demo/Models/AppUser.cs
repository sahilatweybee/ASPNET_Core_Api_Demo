using Microsoft.AspNetCore.Identity;

namespace ASPNET_Core_Books_Api_Demo.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
