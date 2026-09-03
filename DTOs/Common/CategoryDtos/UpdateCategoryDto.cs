namespace gamevault_backend.DTOs.Category;

public class UpdateCategoryDto
{
    public string? CategoryTitle { get; set; }
    public IFormFile? CategoryImage { get; set; }
}