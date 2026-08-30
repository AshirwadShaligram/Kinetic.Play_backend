namespace gamevault_backend.Models;

public class Deal
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? DealImage { get; set; }
    public DealType DealType { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public int? PromoCode { get; set; }
    public int? UsageLimit { get; set; }
    public int UsedCount { get; set; } = 0;
    public int? PerUserLimit { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public DealStatus Status { get; set; }

    // User foriegn key
    public Guid CreatedByAdminId { get; set; }
    public User CreatedByAdmin { get; set; } = null!;

    // Productbelonging to this deal
    public ICollection<Product> Products { get; set; } = new List<Product>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}