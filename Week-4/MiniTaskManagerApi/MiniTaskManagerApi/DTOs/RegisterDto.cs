using System.ComponentModel.DataAnnotations;

namespace MiniTaskManagerApi.DTOs;

public class RegisterDto
{
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}
