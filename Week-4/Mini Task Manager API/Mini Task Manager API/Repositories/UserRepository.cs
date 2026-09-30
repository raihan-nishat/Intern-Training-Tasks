using Mini_Task_Manager_API.Models;
using Mini_Task_Manager_API.Repositories.Interfaces;

namespace Mini_Task_Manager_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> users = new();

        private int nextId = 1;

        public User? GetByUsername(string username)
        {
            return users.FirstOrDefault(
                u => u.username.Equals(
                    username, 
                    StringComparison.OrdinalIgnoreCase
                    )
                );
        }

        public User? GetById(int id)
        {
            return users.FirstOrDefault(u => u.id == id);
        }

        public void Add(User user)
        {
            user.id = nextId++;

            users.Add(user);
        }

        public bool UsernameExists(string username)
        {
            return users.Any(
                u =>  u.username.Equals( 
                    username, 
                    StringComparison.OrdinalIgnoreCase 
                    )
                );
        }
    }
}
