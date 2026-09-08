namespace gamevault_backend.DTOs.Admin.SubCategory;

public class CreateSubCategory
{
    public string Name { get; set; } = "";
    public Guid CategoryId { get; set; }
}