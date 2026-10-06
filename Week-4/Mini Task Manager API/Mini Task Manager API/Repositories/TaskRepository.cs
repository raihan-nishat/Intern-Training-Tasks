using Mini_Task_Manager_API.Data;
using Mini_Task_Manager_API.Models;
using Mini_Task_Manager_API.Repositories.Interfaces;

namespace Mini_Task_Manager_API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext context;

        public TaskRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<TaskItem> GetAll()
        {
            return context.Tasks.ToList();
        }

        public List<TaskItem> GetByOwner(string username)
        {
            return context.Tasks
                .Where(t => t.ownerUsername == username)
                .ToList();
        }

        public TaskItem? GetById(int id)
        {
            return context.Tasks
                .FirstOrDefault(t => t.id == id);
        }

        public void Add(TaskItem task)
        {
            context.Tasks.Add(task);
            context.SaveChanges();
        }

        public bool Update(TaskItem task)
        {
            var existingTask = GetById(task.id);

            if (existingTask == null)
            {
                return false;
            }

            existingTask.title = task.title;
            existingTask.description = task.description;
            existingTask.isCompleted = task.isCompleted;

            context.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var task = GetById(id);

            if (task == null)
            {
                return false;
            }

            context.Tasks.Remove(task);
            context.SaveChanges();

            return true;
        }
    }
}