using Mini_Task_Manager_API.Models;

namespace Mini_Task_Manager_API.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
