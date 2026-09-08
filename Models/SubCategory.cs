namespace gamevault_backend.Models;

public class SubCategory
{

    // BASIC INFO
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsVisible { get; set; }

    // CATEGORY RELATIONSHIP WITH SUBCATEGORY
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    // PRODUCT RELATIONSHIP WITH SUB-CATEGORY
    public ICollection<Product> Products { get; set; } = new List<Product>();
}