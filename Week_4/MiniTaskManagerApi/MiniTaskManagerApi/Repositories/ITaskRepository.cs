using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Repositories
{
    public interface ITaskRepository
    {
        List<TaskItem> GetAll();
        List<TaskItem> GetByOwner(string username);
        TaskItem? GetById(int id);
        TaskItem Add(TaskItem task);
        bool Update(TaskItem task);
        bool Delete(int id);
    }
}
