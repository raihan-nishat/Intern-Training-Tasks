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
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto)
        {
            if (userRepository.UserNameExists(dto.username))
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
                username = dto.username,
                role = "user"
            };

            user.passwordHash =
                passwordHasher.HashPassword(
                user,
                dto.password
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
        public IActionResult Login(LoginDto login)
        {
            var user = userRepository.GetUserName(dto.username);

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
                dto.password
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
