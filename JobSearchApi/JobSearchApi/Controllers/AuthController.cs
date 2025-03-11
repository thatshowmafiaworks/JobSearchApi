using JobSearchApi.Models.DTO;
using JobSearchApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobSearchApi.Controllers
{
    [Route("auth")]
    public class AuthController(
        ITokenService tokenService,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signinManager
    ) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized($"not found user with email: {model.Email}");
            }

            var result = await signinManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized($"wrong password");
            }

            var roles = await userManager.GetRolesAsync(user);
            var token = tokenService.GenerateToken(user, roles);

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser { Email = model.Email, UserName = model.Email};
            var result = await userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded)
            {
                return Unauthorized(result.Errors);
            }

            return Ok(new { message = "User Created succesfully" });
        }


    }
}
