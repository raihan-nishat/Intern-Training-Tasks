
using Microsoft.AspNetCore.Identity;
using MiniTaskManagerApi.Models;

namespace MiniTaskManagerApi.Services;

public class PasswordService
{
    private readonly PasswordHasher<User> _hasher = new();

    public string HashPassword(User user, string password)
    {
        return _hasher.HashPassword(user, password);
    }

    public bool VerifyPassword(
        User user,
        string hashedPassword,
        string providedPassword)
    {
        return _hasher.VerifyHashedPassword(
            user,
            hashedPassword,
            providedPassword
        ) != PasswordVerificationResult.Failed;
    }
}