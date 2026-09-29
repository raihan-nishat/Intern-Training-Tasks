using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public User? GetByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public User Add(User user)
        {
            user.Id = _users.Count == 0 ? 1 : _users.Max(u => u.Id) + 1;
            _users.Add(user);
            return user;
        }
    }
}
