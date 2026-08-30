using System.ComponentModel.DataAnnotations;
using gamevault_backend.Enums.Role;

namespace gamevault_backend.DTOs.Auth;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set ;}
}