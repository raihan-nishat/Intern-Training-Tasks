using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mini_Task_Manager_API.DTOs.Auth;
using Mini_Task_Manager_API.Models;
using Mini_Task_Manager_API.Repositories.Interfaces;
using Mini_Task_Manager_API.Services.Interfaces;

namespace Mini_Task_Manager_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;
        private readonly PasswordHasher<User> passwordHasher;

        public AuthController(IUserRepository userRepository, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;

            passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (userRepository.UserNameExists(dto.UserName))
            {
                return BadRequest(
                    new
                    {
                        message = "username already exists"
                    }
                  );
            }

            var user = new User
            {
                username = dto.UserName,
                role = "user"
            };

            user.passwordHash =
                passwordHasher.HashPassword(
                user,
                dto.Password
                );

            userRepository.Add(user);

            return Ok(
                new
                {
                    message = "user registered successfully"
                }
            );
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = userRepository.GetUserName(dto.Username);

            if(user == null)
            {
                return Unauthorized(
                    new
                    {
                        message = "invalid username or password"
                    }
                );
            }

            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.passwordHash,
                dto.Password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized(
                    new
                    {
                        message = "invalid username or password"
                    }

                 );
            }

            var token = tokenService.CreateToken(user);

            return Ok(
                new
                {
                    token = token
                }
            );
        }
    }
}
