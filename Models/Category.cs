namespace gamevault_backend.Models;

public class Category
{
    public Guid Id { get; set; }

    // BASIC INFO
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string ImagePublicId { get; set; } = string.Empty;

    public int ActiveProducts { get; set; }
    public string Logo { get; set; } = string.Empty;

    public bool IsVisible { get; set; }

    // PRODUCT RELATIONSHIP WITH CATEGORY
    public ICollection<Product> Products { get; set; } = new List<Product>();

    // SUB-CATEGORY RELATIONSHIP WITH CATEGORY 
    public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}