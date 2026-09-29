using Mini_Task_Manager_API.Models;
using Mini_Task_Manager_API.Repositories.Interfaces;

namespace Mini_Task_Manager_API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> tasks = new();

        private int nextId = 1;

        public List<TaskItem> GetAll()
        {
            return tasks;
        }

        public List<TaskItem> GetByOwner(string username)
        {
            return tasks
                .Where(t => t.ownerUsername == username)
                .ToList();
        }

        public TaskItem? GetById(int id)
        {
            return tasks.FirstOrDefault(t => t.id == id);
        }

        public void Add(TaskItem task)
        {
            task.id= nextId++;

            tasks.Add(task);
        }

        public bool Update(TaskItem task)
        {
            var existingTask = GetById(task.id);

            if(existingTask == null)
            {
                return false;
            }

            existingTask.title = task.title;
            existingTask.description = task.description;
            existingTask.isCompleted = task.isCompleted;

            return true;
        }
    }
}
