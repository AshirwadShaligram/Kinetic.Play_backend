namespace gamevault_backend.Models;

public class SellerProfile
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    // Owner information
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string? PhoneNumber { get; set; }

    // Shop information
    public string ShopName { get; set; } = "";
    public string? ShopDescription { get; set; }
    public string? ShopLogoUrl { get; set; }
    public string? ShopBannerUrl { get; set; }

    // Shop contact
    public string? ShopPhoneNumber { get; set; }
    public string? ShopEmail { get; set; }

    // Shop address
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}