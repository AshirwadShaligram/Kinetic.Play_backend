namespace gamevault_backend.DTOs.Admin.SubCategory;

public class SubCategoryResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public bool IsVisible { get; set; }
    public Guid CategoryId { get; set; }
}