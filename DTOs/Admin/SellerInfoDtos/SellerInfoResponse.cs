using gamevault_backend.Enums.Role;

namespace gamevault_backend.DTOs.Admin.SellerInfo;

public class SellerInfoResponseDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public SellerProfileResponseDto? SellerProfile { get; set; }
}