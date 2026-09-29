
using System.ComponentModel.DataAnnotations;

namespace MiniTaskManagerApi.DTOs;

public class TaskCreateDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
}