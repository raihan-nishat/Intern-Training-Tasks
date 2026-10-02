using MiniTaskManagerApi.Models;
using MiniTaskManagerApi.Repositories.Interfaces;

namespace MiniTaskManagerApi.Repositories;
public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new ();

    public List<User> GetAll()
    {
        return _users;
    }

    public User? GetByUsername(string username)
    {
        return _users.FirstOrDefault(u => u.Username == username);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}