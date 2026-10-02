using MiniTaskManagerApi.Models;
using MiniTaskManagerApi.Repositories.Interfaces;

namespace MiniTaskManagerApi.Repositories;
public class TaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new ();

    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public void Add(TaskItem task)
    {
        _tasks.Add(task);
    }

    
public void Update(TaskItem task)
{
    var existingTask = GetById(task.Id);

    if (existingTask != null)
    {
        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.IsCompleted = task.IsCompleted;
    }
}

    public void Delete(int id)
    {
        var task = GetById(id);
        if (task != null)
        {
            _tasks.Remove(task);
        }
    }
}   