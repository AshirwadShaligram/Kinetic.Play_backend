using gamevault_backend.Enums.Role;

namespace gamevault_backend.DTOs.Admin.CustomerInfo;

public class CustomerInfoResponseDto
{
    public Guid Id { get; set; }

    // Basic Info
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public CustomerProfileResponseDto? CustomerProfile { get; set; }
}