using System.ComponentModel.DataAnnotations;

namespace Mini_Task_Manager_API.DTOs.Tasks
{
    public class TaskCreateDto
    {
        [Required]
        [StringLength(100)]
        public string title { get; set; } = string.Empty;

        [StringLength(500)]
        public string description { get; set; } = string.Empty;
    }
}
