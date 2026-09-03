namespace gamevault_backend.DTOs.Category;

public class CreateCategoryDto
{
   
    public string CategoryTitle { get; set; } = string.Empty;
    public IFormFile CategoryImage { get; set; } = null!;
}