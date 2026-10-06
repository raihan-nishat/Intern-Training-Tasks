using Mini_Task_Manager_API.Data;
using Mini_Task_Manager_API.Models;
using Mini_Task_Manager_API.Repositories.Interfaces;

namespace Mini_Task_Manager_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        private int nextId = 1;

        public User? GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(
                u => u.username.Equals(
                    username.ToLower() == username.ToLower())
                );
        }

        public User? GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.id == id);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool UsernameExists(string username)
        {
            return _context.Users.Any(
                u =>  u.username.ToLower() == username.ToLower());
        }
    }
}
