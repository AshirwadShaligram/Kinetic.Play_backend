namespace gamevault_backend.Models;

public class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string[] Tags { get; set; } = Array.Empty<string>();

    public string[] ImageUrls { get; set; } = Array.Empty<string>();
    public string TitleImageUrl { get; set; } = string.Empty;

    // Category Foriegn Key
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null;

    // Seller Foriegn Key
    public Guid UserId { get; set; }

    // Deal Foriegn Key
    public Guid? DealId { get; set; }
    public Deal? Deal { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}