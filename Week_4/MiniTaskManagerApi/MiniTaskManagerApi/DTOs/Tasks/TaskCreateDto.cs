using System.ComponentModel.DataAnnotations;

namespace MiniTaskManagerApi.DTOs.Tasks
{
    public class TaskCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Description { get; set; } = string.Empty;
    }
}
