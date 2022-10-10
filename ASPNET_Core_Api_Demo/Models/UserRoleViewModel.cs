using System;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_Core_Books_Api_Demo.Models
{
    public class UserRoleViewModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        public String Role { get; set; }
    }
}
