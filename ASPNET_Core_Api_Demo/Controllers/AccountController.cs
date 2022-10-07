using ASPNET_Core_Books_Api_Demo.Enums;
using ASPNET_Core_Books_Api_Demo.Models;
using ASPNET_Core_Books_Api_Demo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core_Books_Api_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _AccountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            _AccountRepo = accountRepo;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModl)
        {
            var result = await _AccountRepo.SignUpAsync(signUpModl);
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }
            return Unauthorized();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LogInModel loginModl)
        {
            var result = await _AccountRepo.LogInAsync(loginModl);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
        [Authorize]
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _AccountRepo.LogOutAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
               }

            return Ok();
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromForm]string roleModl)
        {
            await _AccountRepo.AddRoleAsync(roleModl);
            return Ok(roleModl);
        }
        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole(UserRoleViewModel userRoleModl)
        {
            await _AccountRepo.AssignRole(userRoleModl);
            return Ok(userRoleModl);
        }
    }
}
