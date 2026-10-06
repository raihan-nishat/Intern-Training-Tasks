using Mini_Task_Manager_API.Data;
using Mini_Task_Manager_API.Models;
using Mini_Task_Manager_API.Repositories.Interfaces;

namespace Mini_Task_Manager_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }

        public User? GetByUsername(string username)
        {
            return context.Users
                .FirstOrDefault(u =>
                    u.username.ToLower() == username.ToLower());
        }

        public User? GetById(int id)
        {
            return context.Users
                .FirstOrDefault(u => u.id == id);
        }

        public void Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public bool UsernameExists(string username)
        {
            return context.Users.Any(u =>
                u.username.ToLower() == username.ToLower());
        }
    }
}