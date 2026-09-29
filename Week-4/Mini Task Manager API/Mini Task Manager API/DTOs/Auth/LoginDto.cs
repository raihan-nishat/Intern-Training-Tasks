using System.ComponentModel.DataAnnotations;

namespace Mini_Task_Manager_API.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required] 
        public string Password { get; set; } = string.Empty;
    }
}
