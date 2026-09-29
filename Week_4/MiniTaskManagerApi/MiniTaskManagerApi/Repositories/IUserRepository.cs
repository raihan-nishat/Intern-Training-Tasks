using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Repositories
{
    public interface IUserRepository
    {
        User? GetByUsername(string username);
        User Add(User user);
    }
}
