using MiniTaskManagerApi.Interfaces;
using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public List<TaskItem> GetAll()
    {
        lock (_lock)
        {
            return _tasks.Select(t => Copy(t)).ToList();
        }
    }

    public TaskItem? GetById(int id)
    {
        lock (_lock)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            return task == null ? null : Copy(task);
        }
    }

    public TaskItem Add(TaskItem task)
    {
        lock (_lock)
        {
            task.Id = _nextId++;

            _tasks.Add(Copy(task));

            return Copy(task);
        }
    }

    public bool Update(TaskItem task)
    {
        lock (_lock)
        {
            var existing = _tasks.FirstOrDefault(
                t => t.Id == task.Id);

            if (existing == null)
                return false;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.IsCompleted = task.IsCompleted;

            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return false;

            return _tasks.Remove(task);
        }
    }

    private static TaskItem Copy(TaskItem t)
    {
        return new TaskItem
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            IsCompleted = t.IsCompleted,
            CreatedAt = t.CreatedAt,
            OwnerUsername = t.OwnerUsername
        };
    }
}
