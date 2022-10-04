using ASPNET_Core_Books_Api_Demo.Models;
using ASPNET_Core_Books_Api_Demo.Repository;
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
        public async Task<IActionResult> SignUp([FromBody]SignUpModel signUpModl)
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
    }
}
