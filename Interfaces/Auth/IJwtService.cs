using gamevault_backend.Models;

namespace gamevault_backend.Services.Auth;

public interface IJwtService
{
    string GenerateToken(User user);
}