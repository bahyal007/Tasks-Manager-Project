using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Project_part2_Server.DTOs;
using System.IdentityModel.Tokens.Jwt;
using TaskModel;

namespace Project_part2_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<TaskUser> _userManager;
        private readonly  JWTHandler _jwthandler;

        public AccountController(UserManager<TaskUser> userManager, JWTHandler jwthandler)
        {
            _userManager = userManager;
            _jwthandler = jwthandler;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            TaskUser? user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return Unauthorized(new LoginResult
                {
                    Success = false,
                    Message = "Invalid Username or Password."
                });
            }

            JwtSecurityToken secToken = await _jwthandler.GetTokenAsync(user);
            string? jwt = new JwtSecurityTokenHandler().WriteToken(secToken);
            return Ok(new LoginResult
            {
                Success = true,
                Message = "Login successful",
                Token = jwt
            });
        }
    }
}
