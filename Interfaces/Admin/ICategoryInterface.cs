using gamevault_backend.DTOs.Admin.Category;

namespace gamevault_backend.Services.Admin;

public interface ICategoryInterface
{
    Task<List<CategoryResponseDto>> GetAllCategoriesAsync();
    Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto dto);
    Task<CategoryResponseDto?> GetCategoryByIdAsync(Guid id);
    Task<CategoryResponseDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto dto);
    Task<bool> DeleteCategoryAsync(Guid id); 
    Task<bool> ToggleCategoryVisibilityAsync(Guid id);  
}