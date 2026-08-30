namespace gamevault_backend.Models;

public class CustomerProfile
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    // BASIC INFO
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string? PhoneNumber { get; set; }

    // ADDRESS
    public string? AddressLine1 { get; set; } 
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}