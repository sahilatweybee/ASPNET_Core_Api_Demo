using System.ComponentModel.DataAnnotations;

namespace ASPNET_Core_Books_Api_Demo.Models
{
    public class LogInModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
