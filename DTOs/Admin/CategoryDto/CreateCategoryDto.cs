namespace gamevault_backend.DTOs.Admin.Category;

public class CreateCategoryDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = null!;
    public string Logo { get; set; } = string.Empty;

}