namespace Mini_Task_Manager_API.Models
{
    public class TaskItem
    {
        public int id {  get; set; }
        
        public string title { get; set; } = string.Empty;

        public string description { get; set; } = string.Empty;

        public string isCompleted {  get; set; }

        public DateTime createdAt {  get; set; }

        public string ownerUsername { get; set; } = string.Empty;

    }
}
