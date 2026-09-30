using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniTaskManagerApi.DTOs.Auth;
using MiniTaskManagerApi.Models;
using MiniTaskManagerApi.Repositories;
using MiniTaskManagerApi.Services;

namespace MiniTaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(IUserRepository userRepository, ITokenService tokenService, PasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var existingUser = _userRepository.GetByUsername(dto.Username);
            if (existingUser != null)
            {
                return Conflict("Username already exists.");
            }

            var user = new User
            {
                Username = dto.Username,
                Role = "User" 
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            _userRepository.Add(user);
            return Ok(new
            {message = "User registered successfully."});

        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
           var user = _userRepository.GetByUsername(dto.Username);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid username or password.");
            }
            var token = _tokenService.CreateToken(user);
            return Ok(new { token });
        }

    }
}
