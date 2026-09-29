using Mini_Task_Manager_API.Models;

namespace Mini_Task_Manager_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetUserName(string username);

        User? GetById(int id);

        void Add(User user);

        bool UserNameExists(string username);
    }
}
