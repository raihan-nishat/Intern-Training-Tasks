using MiniTaskManagerApi.Interfaces;
using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public List<User> GetAll()
    {
        lock (_lock)
        {
            return _users.Select(u => new User
            {
                Id = u.Id,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                Role = u.Role
            }).ToList();
        }
    }

    public User? GetByUsername(string username)
    {
        lock (_lock)
        {
            var user = _users.FirstOrDefault(u =>
                u.Username.Equals(
                    username,
                    StringComparison.OrdinalIgnoreCase));

            if (user == null)
                return null;

            return new User
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Role = user.Role
            };
        }
    }

    public User Add(User user)
    {
        lock (_lock)
        {
            user.Id = _nextId++;
            _users.Add(user);

            return user;
        }
    }
}