using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
