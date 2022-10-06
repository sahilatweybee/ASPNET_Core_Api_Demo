using ASPNET_Core_Books_Api_Demo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ASPNET_Core_Books_Api_Demo.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _SigninManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly IConfiguration _Configuration;

        public AccountRepo(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _UserManager = userManager;
            _SigninManager = signInManager;
            _Configuration = configuration;
            _RoleManager = roleManager;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModl)
        {
            var user = new AppUser()
            {
                FirstName = signUpModl.FirstName,
                LastName = signUpModl.LastName,
                UserName = signUpModl.Email,
                Email = signUpModl.Email

            };
            return await _UserManager.CreateAsync(user, signUpModl.Password);
        }

        public async Task<string> LogInAsync(LogInModel logInModl)
        {
            var result = await _SigninManager.PasswordSignInAsync(logInModl.UserName, logInModl.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, logInModl.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var AuthKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_Configuration["JWT:Secret"]));

            var Token = new JwtSecurityToken(
                issuer: _Configuration["JWT:ValidIssuer"],
                audience: _Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token: Token);
        }

        public async Task LogOutAsync()
        {
            await _SigninManager.SignOutAsync();
        }

        public async Task AddRoleAsync(string roleModl)
        {
            IdentityRole role = new IdentityRole
            {
                Name = roleModl
            };
            await _RoleManager.CreateAsync(role);
        }

        public async Task MakeAdminAsync(string userName)
        {
            var user = await _UserManager.FindByNameAsync(userName);
            await _UserManager.AddToRoleAsync(user, "Admin");
        }
    }
}
