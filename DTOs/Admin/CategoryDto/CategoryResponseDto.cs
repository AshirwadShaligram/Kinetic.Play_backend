using gamevault_backend.DTOs.Admin.SubCategory;

namespace gamevault_backend.DTOs.Admin.Category;

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int ActiveProducts { get; set; }
    public string Image { get; set; } = "";
    public string Logo { get; set; } = "";
    public bool IsVisible { get; set; }
    public List<SubCategoryResponseDto> SubCategories { get; set; } = new();
}