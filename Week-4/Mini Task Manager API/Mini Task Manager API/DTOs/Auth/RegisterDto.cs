using System.ComponentModel.DataAnnotations;

namespace Mini_Task_Manager_API.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
