using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Interfaces;

public interface IUserRepository
{
    List<User> GetAll();

    User? GetByUsername(string username);

    User Add(User user);
}

