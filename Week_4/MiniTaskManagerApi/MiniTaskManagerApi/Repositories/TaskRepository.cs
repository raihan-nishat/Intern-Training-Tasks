using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new();

        public List<TaskItem> GetAll()
        {
            return _tasks;
        }

        public List<TaskItem> GetByOwner(string username)
        {
            return _tasks.Where(t => t.OwnerUsername.Equals(username, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public TaskItem? GetById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public TaskItem Add(TaskItem task)
        {
            task.Id = _tasks.Count == 0 ? 1 : _tasks.Max(t => t.Id) + 1;
            _tasks.Add(task);
            return task;
        }

        public bool Update(TaskItem task)
        {
            TaskItem? existingTask = GetById(task.Id);
            if (existingTask == null)
            {
                return false;
            }
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            return true;
        }

        public bool Delete(int id)
        {
            TaskItem? task = GetById(id);
            if (task == null)
            {
                return false;
            }
            _tasks.Remove(task);
            return true;
        }

    }
}
