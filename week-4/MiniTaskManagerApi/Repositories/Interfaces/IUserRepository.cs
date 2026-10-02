using MiniTaskManagerApi.Models; // to use the User model
namespace MiniTaskManagerApi.Repositories.Interfaces; // to define the IUserRepository interface
public interface IUserRepository
{
    List<User> GetAll(); // to get all users
    User? GetByUsername(string username); // to get a user by username
    void Add(User user); // to add a new user
}