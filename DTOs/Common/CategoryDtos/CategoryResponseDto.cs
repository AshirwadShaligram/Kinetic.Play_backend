namespace gamevault_backend.DTOs.Category;

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public string CategoryTitle { get; set; } = "";
    public string CategoryImage { get; set; } = "";
}