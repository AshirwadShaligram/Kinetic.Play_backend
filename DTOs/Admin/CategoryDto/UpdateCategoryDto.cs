namespace gamevault_backend.DTOs.Admin.Category;

public class UpdateCategoryDto
{
     public string? Title { get; set; } 
    public string? Description { get; set; } 
    public IFormFile? Image { get; set; }
    public string? Logo { get; set; } 
}   