namespace gamevault_backend.Models;

public class Category
{
    public Guid Id { get; set; }

    public string CategoryTitle { get; set; } = string.Empty;

    public string CategoryImage { get; set; } = string.Empty;

    public string CategoryImagePublicId { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public ICollection<SubCategory> SubCategories { get; set; } =
        new List<SubCategory>();
}