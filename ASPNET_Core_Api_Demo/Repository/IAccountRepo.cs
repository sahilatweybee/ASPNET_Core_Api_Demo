using ASPNET_Core_Books_Api_Demo.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ASPNET_Core_Books_Api_Demo.Repository
{
    public interface IAccountRepo
    {
        Task AddRoleAsync(string roleModl);
        Task<string> LogInAsync(LogInModel logInModl);
        Task LogOutAsync();
        Task AssignRole(UserRoleViewModel roleModl);
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModl);
    }
}