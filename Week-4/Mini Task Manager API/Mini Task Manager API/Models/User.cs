namespace Mini_Task_Manager_API.Models
{
    public class User
    {
        public int id {  get; set; }
        
        public string username { get; set; } = string.Empty;
        
        public string passwordHash { get; set; } = string.Empty;
        
        public string role { get; set; } = "user";
    }
}
