namespace gamevault_backend.Models;

public class SubCategory
{
    public Guid Id { get; set; }
    public string SubCategoryName { get; set; } = string.Empty;
    public bool IsVisible { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}