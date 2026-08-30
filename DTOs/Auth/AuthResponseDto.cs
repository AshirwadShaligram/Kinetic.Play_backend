using gamevault_backend.Enums.Role;

namespace gamevault_backend.DTOs.Auth;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    public string Email { get; set; } = "";
    public Role Role { get; set; }
}