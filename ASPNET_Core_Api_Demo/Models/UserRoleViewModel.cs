using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core_Books_Api_Demo.Models
{
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        
        [Required][EmailAddress]
        public string UserName { get; set; }
    }
}
