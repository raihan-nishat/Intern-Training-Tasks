using Mini_Task_Manager_API.Models;

namespace Mini_Task_Manager_API.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        List<TaskItem> GetAll();

        List<TaskItem> GetByOwner(string username);

        TaskItem? GetById(int  id);

        void Add(TaskItem item);

        bool Update(TaskItem item);

        bool Delete(int id);
    }
}
