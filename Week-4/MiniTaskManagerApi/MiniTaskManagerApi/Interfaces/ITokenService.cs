using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}