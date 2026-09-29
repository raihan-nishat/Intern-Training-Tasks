namespace Mini_Task_Manager_API.DTOs.Tasks
{
    public class TaskResponseDto
    {
        public int id {  get; set; }

        public string title { get; set; } = string.Empty;
        
        public string description {  get; set; } = string.Empty;

        public bool isCompleted { get; set; }

        public DateTime createAt { get; set; }

        public string ownerUsername { get; set; } = string.Empty;
    }
}
