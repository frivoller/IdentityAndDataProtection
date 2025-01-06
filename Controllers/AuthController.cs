using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityAndDataProtection.DTOs;
using IdentityAndDataProtection.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;
using YourNamespace.Models;

namespace IdentityAndDataProtection.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = register.UserName,
                Email = register.Email
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                return Ok(user);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
                _logger.LogError(error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user is null)
            {
                _logger.LogError("User not found");
                return BadRequest("Invalid login attempt.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in successfully.");
                return Ok(user);
            }
            else
            {
                _logger.LogError("Invalid password attempt.");
                return BadRequest("Invalid login attempt.");
            }
        }

        private void LogModelErrors()
        {
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }
        }
    }

}