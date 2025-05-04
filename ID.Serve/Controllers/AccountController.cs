using ID.Serve.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ID.Serve.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = new AppUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    PhoneNumber = dto.PhoneNumber
                };
                var result = await userManager.CreateAsync(user, dto.Password);
                return result.Succeeded ? Ok("Registered") : BadRequest(result.Errors);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpGet("change-password")]
        public async Task<IActionResult> ChangePasswordAsync()
        {
            var user = await userManager.FindByNameAsync("khara");
            if (user == null) return NotFound("User not found");
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, "Milepass@42");
            return result.Succeeded? Ok("Password changed"):BadRequest(result.Errors);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserProfileDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await userManager.FindByNameAsync(dto.UserName);
                if (user == null) return NotFound();

                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Email = dto.Email;
                user.PhoneNumber = dto.PhoneNumber;

                var result = await userManager.UpdateAsync(user);
                return result.Succeeded ? Ok("Updated") : BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await userManager.FindByNameAsync(dto.UserName);
                if (user == null) return NotFound();

                var result = await signInManager.PasswordSignInAsync(user, dto.Password, false, false);
                return result.Succeeded ? Ok("Login successful") : Unauthorized("Invalid login");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
                return Ok("Logged out");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
