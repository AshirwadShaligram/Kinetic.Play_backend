using gamevault_backend.DTOs.Auth;
using gamevault_backend.Models;

namespace gamevault_backend.Services.Auth;

public interface IAuthService
{
    Task<User> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RefreshAsync(string refreshToken);
}