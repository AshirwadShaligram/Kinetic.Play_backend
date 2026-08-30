using gamevault_backend.Enums.Role;

namespace gamevault_backend.Models;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public Role Role { get; set; }
    public bool IsActive { get; set; }
    public string RefreshToken { get; set; } = "";
    public DateTime RefreshTokenExpiredDate { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public CustomerProfile? CustomerProfile { get; set; }
    public SellerProfile? SellerProfile { get; set; }
}